using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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

		public static OperationQuestionScore RunTest(int numberOfQuestionsLeft, UserDifficulty userDifficulty)
		{
			Random random = new Random();
			var (operationMin, operationMax) = GetPossibleOperationsByDifficulty(userDifficulty);
			var score = new OperationQuestionScore();
			while (numberOfQuestionsLeft > 0)
			{
				int mathRandomOperation = random.Next(operationMin, operationMax);
				MathOperation mathOperation = (MathOperation)mathRandomOperation;
				var (message, correctAnswer) = GetMathsEquation(mathOperation, userDifficulty);
				if (mathRandomOperation == 4 || mathRandomOperation == 6)
				{
					Console.Write($"To the nearest integer, What is {message} =");
				}
				else
				{
					Console.Write($"What is {message} =");
				}

				double userAnswer = Convert.ToDouble(Console.ReadLine());
				if (Math.Round(correctAnswer) == userAnswer)
				{
					Console.WriteLine("Well Done!");
					score.Increment(mathOperation, true);
				}
				else
				{
					Console.WriteLine("Your answer is incorrect!");
					score.Increment(mathOperation, false);
				}
				numberOfQuestionsLeft--;
			}
			return score;
		}

		static (UserDifficulty, int) UserInputs()
		{
			Dictionary<string, UserDifficulty> difficultyDictionary = new Dictionary<string, UserDifficulty>();
			difficultyDictionary.Add("E", UserDifficulty.Easy);
			difficultyDictionary.Add("N", UserDifficulty.Normal);
			difficultyDictionary.Add("H", UserDifficulty.Hard);

			string userInputDifficulty;
			int numberOfQuestions;

			do
			{
				Console.WriteLine("What difficulty level would you like to do! Please type E for Easy, N for Normal and H for hard");
				userInputDifficulty = Console.ReadLine().ToUpper();
			} while (userInputDifficulty != "E" && userInputDifficulty != "N" && userInputDifficulty != "H");

			UserDifficulty userDifficulty = difficultyDictionary[userInputDifficulty];

			do
			{
				Console.WriteLine("How many questions would you like to answer? Please type a number divisible by 10!");
				int.TryParse(Console.ReadLine(), out numberOfQuestions);
			} while (numberOfQuestions % 10 != 0);

			return (userDifficulty, numberOfQuestions);
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

        public class PrntToFile
        {

        }

        public static void SuggestingDifficulty()
        {
			ToFile objnew = ToFile.Deserialize();

            Console.WriteLine($"Last time you did the test on {objnew.UserDifficulty} level and got {objnew.TotalScore}/{objnew.NumberOfQuestions}");
			double decimalScore = (double)objnew.TotalScore / (double)objnew.NumberOfQuestions;

			if (objnew.UserDifficulty == UserDifficulty.Easy)
				{
				if (decimalScore <= 0.7)
				{
					Console.WriteLine($"You should stay on Easy difficulty");
				}
				else
				{
					Console.WriteLine($"Easy difficulty seems to easy for you! You should go up to Normal difficulty");
				}
			}
				else if (objnew.UserDifficulty == UserDifficulty.Normal)
			{
				if (decimalScore <= 0.3)
				{
					Console.WriteLine($"Normal difficulty seems to be to hard for you:( You should go down to Easy difficulty");
				}
				else if ((decimalScore > 0.3) && (decimalScore <= 0.7))
				{
					Console.WriteLine($"You should stay on Normal difficulty");
				}
				else
				{
					Console.WriteLine($"Normal difficulty seems to easy for you! You should go up to Hard difficulty");
				}
			}
			else if (objnew.UserDifficulty == UserDifficulty.Hard)
			{
				if (decimalScore <= 0.3)
				{
					Console.WriteLine($"Hard difficulty seems to hard for you:( You should go down to Normal difficulty");
				}
				else if ((decimalScore > 0.3) && (decimalScore <= 0.8))
				{
					Console.WriteLine($"You should stay on Hard difficulty");
				}
				else
				{
					Console.WriteLine($"You are a maths Genius! Sadly this is the hardest level");
				}
			}
			Console.ReadKey();
			Console.Write(Environment.NewLine);
		}

			public static void Main(string[] args)
			{
			    SuggestingDifficulty();

				var (userDifficulty, numberOfQuestions) = UserInputs();
			    OperationQuestionScore score = RunTest(numberOfQuestions, userDifficulty);
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
			    ToFile.Serialize(numberOfQuestions, score.TotalScore, userDifficulty);
			}
	}
}