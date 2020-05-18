using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace mathstester
{
	class Program
	{
		public enum UserDifficulty
		{
			Easy,
			Normal,
			Hard
		}

		public enum MathOperation
		{
			Addition = 1,
			Subtraction = 2,
			Multiplication = 3,
			Division = 4,
			Power = 5,
			SquareRoot = 6
		}

		public static (int operationMin, int operationMax) GetPossibleOperationsByDifficulty(UserDifficulty userDifficulty)
		{

            switch (userDifficulty)
			{
				case UserDifficulty.Easy:
					return (1, 4);
				case UserDifficulty.Normal:
					return (1, 5);
				case UserDifficulty.Hard:
					return (3, 7);
				default:
					throw new Exception();
			}
		}

		public static (string message, double correctAnswer) GetMathsEquation(MathOperation mathOperation, UserDifficulty userDifficulty)
		{
			int number1;
			int number2;
			Random randomNumber = new Random();

			switch (mathOperation)
			{
				case MathOperation.Addition:
					number1 = randomNumber.Next(1000);
					number2 = randomNumber.Next(1000);
					return ($"{number1} + {number2}", number1 + number2);
				case MathOperation.Subtraction:
					number1 = randomNumber.Next(1000);
					number2 = randomNumber.Next(1000);
					return ($"{number1} - {number2}", number1 - number2);
				case MathOperation.Multiplication:
					number1 = userDifficulty == UserDifficulty.Easy ? randomNumber.Next(13) : randomNumber.Next(1000);
					number2 = userDifficulty == UserDifficulty.Easy ? randomNumber.Next(13) : randomNumber.Next(1000);
					return ($"{number1} * {number2}", number1 * number2);
				case MathOperation.Division:
					number1 = randomNumber.Next(10000);
					number2 = randomNumber.Next(1000);
					return ($"{number1} / {number2}", number1 / (double)number2);
				case MathOperation.Power:
					number1 = randomNumber.Next(20);
					number2 = randomNumber.Next(5);
					return ($"{number1} ^ {number2}", Math.Pow(number1, number2));
				case MathOperation.SquareRoot:
					number1 = randomNumber.Next(1000);
					return ($"√{number1}", Math.Sqrt(number1));
				default:
					throw new Exception();
			}
		}

		public class OperationQuestionScore
		{
			public int AdditionQuestion { get; private set; }
			public int AdditionScore { get; private set; }
			public int SubtractionQuestion { get; private set; }
			public int SubtractionScore { get; private set; }
			public int MultiplicationQuestion { get; private set; }
			public int MultiplicationScore { get; private set; }
			public int DivisionQuestion { get; private set; }
			public int DivisionScore { get; private set; }
			public int PowerQuestion { get; private set; }
			public int PowerScore { get; private set; }
			public int SquareRootQuestion { get; private set; }
			public int SquareRootScore { get; private set; }
            public int TotalScore { get; private set; }

			public void Increment(MathOperation mathOperation, bool isCorrect)
			{
				if (isCorrect == true)
				{
					switch (mathOperation)
					{
						case MathOperation.Addition:
							AdditionQuestion++;
							AdditionScore++;
							break;
						case MathOperation.Subtraction:
							SubtractionQuestion++;
							SubtractionScore++;
							break;
						case MathOperation.Multiplication:
							MultiplicationQuestion++;
							MultiplicationScore++;
							break;
						case MathOperation.Division:
							DivisionQuestion++;
							DivisionScore++;
							break;
						case MathOperation.Power:
							PowerQuestion++;
							PowerScore++;
							break;
						case MathOperation.SquareRoot:
							SquareRootQuestion++;
							SquareRootScore++;
							break;
					}
					TotalScore++;
				}
				else
				{
					switch (mathOperation)
					{
						case MathOperation.Addition:
							AdditionQuestion++;
							break;
						case MathOperation.Subtraction:
							SubtractionQuestion++;
							break;
						case MathOperation.Multiplication:
							MultiplicationQuestion++;
							break;
						case MathOperation.Division:
							DivisionQuestion++;
							break;
						case MathOperation.Power:
							PowerQuestion++;
							break;
						case MathOperation.SquareRoot:
							SquareRootQuestion++;
							break;
					}
				}
			}
		}

		class RunWithTimer
		{
			public bool IsTimeLeft { get; }
			public static void Timer(int numberOfSeconds)
			{
			    var whenToStop = DateTime.Now.AddSeconds(numberOfSeconds);
				while (DateTime.Now < whenToStop)
				{
					string timeLeft = (whenToStop - DateTime.Now).ToString(@"hh\:mm\:ss");
					WriteToScreen($"Time Remaining: {timeLeft}", true);
					Thread.Sleep(1000);
				}
			}

			public RunWithTimer(int numberOfSeconds)
			{
				Thread timerThread = new Thread(new ThreadStart(() =>
				{
					Timer(numberOfSeconds);
				}));
				timerThread.Start();

				var whenToStop = DateTime.Now.AddSeconds(numberOfSeconds);
				if (DateTime.Now < whenToStop)
				{
					IsTimeLeft = true;
				}
				else
				{
					IsTimeLeft = false;
				}
			}
            /*
            public void StopTimer(int numberOfQuestionsLeft)
			{
				if (numberOfQuestionsLeft == 0)
				{
					timerThread.Abort();
				}
			}
            */
			public static string ReadInput()
			{
				string input = Console.ReadLine();
				Console.Write(new string(' ', 100));
				Console.CursorLeft = 0;
				return input;
			}

			static readonly object lockObject = new object();

			public static void WriteToScreen(string message, bool resetCursor)
			{
				lock (lockObject)
				{
					if (resetCursor)
					{
						int leftPos = Console.CursorLeft;
						Console.WriteLine();
						Console.Write(message.PadRight(50, ' '));
						Console.CursorTop--;
						Console.CursorLeft = leftPos;
					}
					else
					{
						Console.WriteLine(message);
						Console.Write(new string(' ', 100));
						Console.CursorLeft = 0;
					}
				}
			}
		}

		public static OperationQuestionScore RunTest(int numberOfQuestionsLeft, UserDifficulty userDifficulty, int numberOfSeconds)
		{
			Random random = new Random();
			var (operationMin, operationMax) = GetPossibleOperationsByDifficulty(userDifficulty);
			var score = new OperationQuestionScore();
			RunWithTimer RunWithTimer = new RunWithTimer(numberOfSeconds);

			while (numberOfQuestionsLeft > 0 && RunWithTimer.IsTimeLeft)
			{
				int mathRandomOperation = random.Next(operationMin, operationMax);
				MathOperation mathOperation = (MathOperation)mathRandomOperation;
				var (message, correctAnswer) = GetMathsEquation(mathOperation, userDifficulty);
				if (mathRandomOperation == 4 || mathRandomOperation == 6)
				{
					RunWithTimer.WriteToScreen($"To the nearest integer, What is {message} =", false);
				}
				else
				{
					RunWithTimer.WriteToScreen($"What is {message} =", false);
				}

				double userAnswer = Convert.ToDouble(RunWithTimer.ReadInput());
				if (Math.Round(correctAnswer) == userAnswer)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					RunWithTimer.WriteToScreen("Well Done!", false);
					Console.ResetColor();
					score.Increment(mathOperation, true);
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					RunWithTimer.WriteToScreen("Your answer is incorrect!", false);
					Console.ResetColor();
					score.Increment(mathOperation, false);
				}
				numberOfQuestionsLeft--;
			}
			return score;
		}

		static (UserDifficulty, int, string, int) UserInputs()
		{
            Dictionary<string, UserDifficulty> difficultyDictionary = new Dictionary<string, UserDifficulty>
            {
                { "E", UserDifficulty.Easy },
                { "N", UserDifficulty.Normal },
                { "H", UserDifficulty.Hard }
            };

            string userInputDifficulty = "E";
			int numberOfQuestions;
			string autoDifficultyInput;
			int numberOfSeconds;

			do
			{
				Console.WriteLine("Would you like to continue with the suggested difficulty? Please type 'Y' or 'N'");
				autoDifficultyInput = Console.ReadLine().Substring(0).ToUpper();
			} while (autoDifficultyInput != "Y" && autoDifficultyInput != "N");

            if (autoDifficultyInput == "N")
            {
				do
				{
					Console.WriteLine("Which difficulty level would you like to do! Please type E for Easy, N for Normal and H for Hard");
					userInputDifficulty = Console.ReadLine().ToUpper();
				} while (userInputDifficulty != "E" && userInputDifficulty != "N" && userInputDifficulty != "H");
			}

			UserDifficulty userDifficulty = difficultyDictionary[userInputDifficulty];

			do
			{
				Console.WriteLine("How many questions would you like to answer? Please type a number divisible by 10!");
				int.TryParse(Console.ReadLine(), out numberOfQuestions);
			} while (numberOfQuestions % 10 != 0);

			do
			{
				Console.WriteLine("How many seconds would you like the test to be? Please type a number divisible by 30!");
				int.TryParse(Console.ReadLine(), out numberOfSeconds);
			} while (numberOfSeconds % 30 != 0);

			return (userDifficulty, numberOfQuestions, autoDifficultyInput, numberOfSeconds);
		}

		[Serializable]
		public class ToFile
		{
			public int TotalScore { get; private set; }
			public int NumberOfQuestions { get; }
			public UserDifficulty UserDifficulty { get; }
			public ToFile(int numberOfQuestions, UserDifficulty userDifficulty, int totalScore)
			{
				NumberOfQuestions = numberOfQuestions;
				UserDifficulty = userDifficulty;
				TotalScore = totalScore;
			}
		}

        public class SaveToFile
        {
            public static void Serialize(int numberOfQuestions, int totalScore, UserDifficulty userDifficulty)
			{
				ToFile obj = new ToFile(numberOfQuestions, userDifficulty, totalScore);
				IFormatter formatter = new BinaryFormatter();
				Stream stream = new FileStream("D:\\Example.txt", FileMode.Create, FileAccess.Write);
				formatter.Serialize(stream, obj);
				stream.Close();
			}
			public static ToFile Deserialize()
			{
				Stream stream = new FileStream("D:\\Example.txt", FileMode.Open, FileAccess.Read);
				IFormatter formatter = new BinaryFormatter();
				ToFile objnew = (ToFile)formatter.Deserialize(stream);
				stream.Close();
				return objnew;
			}
		}

        public static UserDifficulty SuggestingDifficulty()
        {
			ToFile objnew = SaveToFile.Deserialize();
			UserDifficulty userDifficulty = UserDifficulty.Easy;

			Console.WriteLine($"Last time you did the test on {objnew.UserDifficulty} level and got {objnew.TotalScore}/{objnew.NumberOfQuestions}");
			double decimalScore = (double)objnew.TotalScore / (double)objnew.NumberOfQuestions;

			if (objnew.UserDifficulty == UserDifficulty.Easy)
				{
				if (decimalScore <= 0.7)
				{
					Console.WriteLine($"You should stay on Easy difficulty");
					userDifficulty = UserDifficulty.Easy;
				}
				else
				{
					Console.WriteLine($"Easy difficulty seems to easy for you💪! You should go up to Normal difficulty");
					userDifficulty = UserDifficulty.Normal;
				}
			}
			else if (objnew.UserDifficulty == UserDifficulty.Normal)
			{
				if (decimalScore <= 0.3)
				{
					Console.WriteLine($"Normal difficulty seems to be to hard for you☹️. You should go down to Easy difficulty");
					userDifficulty = UserDifficulty.Easy;
				}
				else if ((decimalScore > 0.3) && (decimalScore <= 0.7))
				{
					Console.WriteLine($"You should stay on Normal difficulty");
					userDifficulty = UserDifficulty.Normal;
				}
				else
				{
					Console.WriteLine($"Normal difficulty seems to easy for you💪! You should go up to Hard difficulty");
					userDifficulty = UserDifficulty.Hard;
				}
			}
			else if (objnew.UserDifficulty == UserDifficulty.Hard)
			{
				if (decimalScore <= 0.3)
				{
					Console.WriteLine($"Hard difficulty seems to hard for you☹️. You should go down to Normal difficulty");
					userDifficulty = UserDifficulty.Normal;
				}
				else if ((decimalScore > 0.3) && (decimalScore <= 0.8))
				{
					Console.WriteLine($"You should stay on Hard difficulty");
					userDifficulty = UserDifficulty.Hard;
				}
				else
				{
					Console.WriteLine($"You are a maths Genius🥳! Sadly this is the hardest level");
					userDifficulty = UserDifficulty.Hard;
				}
			}
			return userDifficulty;
		}

		public static void Main(string[] args)
	    {
		    UserDifficulty userSuggestingDifficulty = SuggestingDifficulty();
            var (userDifficulty, numberOfQuestions, autoDifficultyInput, numberOfSeconds) = UserInputs();

			if (autoDifficultyInput == "Y")
            {
			    userDifficulty = userSuggestingDifficulty;
			}

			var score = RunTest(numberOfQuestions, userDifficulty, numberOfSeconds);

			Console.WriteLine($"Total score: {score.TotalScore} of {numberOfQuestions}");

			if (userDifficulty == UserDifficulty.Easy)
			{
				Console.WriteLine($"Addition score: {score.AdditionScore} of {score.AdditionQuestion}");
                Console.WriteLine($"Subtraction score: {score.SubtractionScore} of {score.SubtractionQuestion}");
				Console.WriteLine($"Multiplication score: {score.MultiplicationScore} of {score.MultiplicationQuestion}");
			}
			else if (userDifficulty == UserDifficulty.Normal)
			{
                Console.WriteLine($"Addition score: {score.AdditionScore} of {score.AdditionQuestion}");
				Console.WriteLine($"Subtraction score: {score.SubtractionScore} of {score.SubtractionQuestion}");
				Console.WriteLine($"Multiplication score: {score.MultiplicationScore} of {score.MultiplicationQuestion}");
				Console.WriteLine($"Division score: {score.DivisionScore} of {score.DivisionQuestion}");
			}
            else if (userDifficulty == UserDifficulty.Hard)
			{
				Console.WriteLine($"Multipication score: {score.MultiplicationScore} of {score.MultiplicationQuestion}");
				Console.WriteLine($"Division score: {score.DivisionScore} of {score.DivisionQuestion}");
                Console.WriteLine($"Power score: {score.PowerScore} of {score.PowerQuestion}");
				Console.WriteLine($"Squareroot score: {score.SquareRootScore} of {score.SquareRootQuestion}");
			}
		    SaveToFile.Serialize(numberOfQuestions, score.TotalScore, userDifficulty);
		}
	}
}