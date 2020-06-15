using System;
using System.Collections.Generic;
using System.IO;
using static mathstester.Calculation;
using static mathstester.SaveLastTestResults;
using static mathstester.UserLogin;


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
					CanUseManyTimes.WriteToScreen($"To the nearest integer, What is {message} =", false);
				}
				else
				{
					CanUseManyTimes.WriteToScreen($"What is {message} =", false);
				}

				double userAnswer = Convert.ToDouble(CanUseManyTimes.ReadInput());
				if (Math.Round(correctAnswer) == userAnswer)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					CanUseManyTimes.WriteToScreen("Well Done!", false);
					Console.ResetColor();
					score.Increment(mathOperation, true);
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					CanUseManyTimes.WriteToScreen("Your answer is incorrect!", false);
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

		public static void Main(string[] args)
	    {
			Console.WriteLine("To Login Type 1, To Create a new account Type 2");
			int LogInOrSignUp;
			do
			{
				int.TryParse(Console.ReadLine(), out LogInOrSignUp);
			} while (LogInOrSignUp != 1 && LogInOrSignUp != 2);

			var filePath = Path.Combine(AppContext.BaseDirectory, "AccountDetails.txt");
			var userName = "";
			var password = "";
			var successfull = false;
			var userDetails = Users.DeserializeAccountDetails(filePath);

			if (userDetails is null)
				userDetails = new Users();

			while (!successfull)
			{
				if (LogInOrSignUp == 1)
				{
					Console.WriteLine("Write your username:");
					userName = Console.ReadLine();
					Console.WriteLine("Enter your password:");
					password = Console.ReadLine();
					if (userDetails.ContainsAccount(userName, password))
					{
						Console.WriteLine("You have logged in successfully!");
						successfull = true;
						break;
					}
					else
					{
						Console.WriteLine("Your username or password is incorect, try again!");
					}
				}
				else
				{
					Console.WriteLine("Enter a username:");
					userName = Console.ReadLine();

					if (userDetails.ContainsUserName(userName))
					{
						Console.WriteLine("The username is taken. Try another one.");
					}
					else
					{
						Console.WriteLine("Enter a password:");
						password = Console.ReadLine();

						successfull = true;
						userDetails.AddAccountDetails(userName, password);
						userDetails.SerializeAccountDetails(filePath);
						Console.WriteLine($"A new account for {userName} has been created!");
					}
				}
			}

			UserDifficulty userSuggestingDifficulty = UserDifficulty.Easy;
			if (File.Exists($"{userName}.txt"))
			{
				userSuggestingDifficulty = CanUseManyTimes.SuggestingDifficulty(userName);
			}
            var (userDifficulty, numberOfQuestions, autoDifficultyInput, numberOfSeconds) = UserInputs();

            if (LogInOrSignUp == 2)
            {
				if (autoDifficultyInput == "Y")
				{
					userDifficulty = userSuggestingDifficulty;
				}
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
		    SaveToFile.SerializeLastTest(numberOfQuestions, score.TotalScore, userDifficulty, userName);
		}
	}
}