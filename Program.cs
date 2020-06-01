using System;
using System.Collections.Generic;
using static mathstester.Calculation;
using static mathstester.SaveLastTestResults;

namespace mathstester
{
	class Program
	{
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
					Printing.WriteToScreen($"To the nearest integer, What is {message} =", false);
				}
				else
				{
					Printing.WriteToScreen($"What is {message} =", false);
				}

				double userAnswer = Convert.ToDouble(Printing.ReadInput());
				if (Math.Round(correctAnswer) == userAnswer)
				{
					Console.ForegroundColor = ConsoleColor.Green;
                    Printing.WriteToScreen("Well Done!", false);
					Console.ResetColor();
					score.Increment(mathOperation, true);
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Printing.WriteToScreen("Your answer is incorrect!", false);
					Console.ResetColor();
					score.Increment(mathOperation, false);
				}
				numberOfQuestionsLeft--;
				RunWithTimer.StopTimer(numberOfQuestionsLeft);
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