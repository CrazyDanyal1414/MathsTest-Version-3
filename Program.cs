using System;
using System.Collections.Generic;

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
					number1 = randomNumber.Next(13);
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
			public int additionQuestion { get; set; }
			public int additionScore { get; set; }
			public int subtractionQuestion { get; set; }
			public int subtractionScore { get; set; }
			public int multiplicationQuestion { get; set; }
			public int multiplicationScore { get; set; }
			public int divisionQuestion { get; set; }
			public int divisionScore { get; set; }
			public int powerQuestion { get; set; }
			public int powerScore { get; set; }
			public int squareRootQuestion { get; set; }
			public int squareRootScore { get; set; }
		}

		public static OperationQuestionScore Score()
        {
			return new OperationQuestionScore();
		}

		public static (int, OperationQuestionScore, OperationQuestionScore) RunTest(int numberOfQuestionsLeft, UserDifficulty userDifficulty)
		{
            int totalScore = 0;
			Random random = new Random();
			var (operationMin, operationMax) = GetPossibleOperationsByDifficulty(userDifficulty);
			var score = Score();
			var question = Score();
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
                    if (mathRandomOperation == 1)
                    {
						question.additionQuestion++;
						score.additionScore++;
					}
					else if (mathRandomOperation == 2)
					{
						question.subtractionQuestion++;
						score.subtractionScore++;
					}
					else if (mathRandomOperation == 3)
					{
						question.multiplicationQuestion++;
						score.multiplicationScore++;
					}
					else if (mathRandomOperation == 4)
					{
						question.divisionQuestion++;
						score.divisionScore++;
					}
					else if (mathRandomOperation == 5)
					{
						question.powerQuestion++;
						score.powerScore++;
					}
                    else
                    {
						question.squareRootQuestion++;
						score.squareRootScore++;
                    }
					totalScore++;
				}
				else
				{
					Console.WriteLine("Your answer is incorrect!");
					if (mathRandomOperation == 1)
					{
						question.additionQuestion++;
					}
					else if (mathRandomOperation == 2)
					{
						question.subtractionQuestion++;
					}
					else if (mathRandomOperation == 3)
					{
						question.multiplicationQuestion++;
					}
					else if (mathRandomOperation == 4)
					{
						question.divisionQuestion++;
					}
					else if (mathRandomOperation == 5)
					{
						question.powerQuestion++;
					}
					else
					{
						question.squareRootQuestion++;
					}
				}
				numberOfQuestionsLeft--;
			}
			return (totalScore, score, question);
		}
		public static void Main(string[] args)
		{
			Dictionary<string, UserDifficulty> dict = new Dictionary<string, UserDifficulty>();
			dict.Add("E", UserDifficulty.Easy);
			dict.Add("N", UserDifficulty.Normal);
			dict.Add("H", UserDifficulty.Hard);

			string userInputDifficulty = "";
			do
			{
				Console.WriteLine("What difficulty level would you like to do! Please type E for Easy, N for Normal and H for hard");
				userInputDifficulty = Console.ReadLine().ToUpper();
			} while (userInputDifficulty != "E" && userInputDifficulty != "N" && userInputDifficulty != "H");

			UserDifficulty userDifficulty = dict[userInputDifficulty];

			int numberOfQuestions = 0;
			do
			{
				Console.WriteLine("How many questions would you like to answer? Please type a number divisible by 10!");
				int.TryParse(Console.ReadLine(), out numberOfQuestions);
			} while (numberOfQuestions % 10 != 0);

			var (totalScore, score, question) = RunTest(numberOfQuestions, userDifficulty);
			Console.WriteLine($"You got a score of {totalScore} out of {numberOfQuestions}");

			if (userDifficulty == UserDifficulty.Easy)
			{
				Console.WriteLine($"You got an addition score of {score.additionScore} out of {question.additionQuestion}");
				Console.WriteLine($"You got an subtraction score of {score.subtractionScore} out of {question.subtractionQuestion}");
				Console.WriteLine($"You got a multiplication score of {score.multiplicationScore} out of {question.multiplicationQuestion}");
			}
			else if (userDifficulty == UserDifficulty.Normal)
			{
				Console.WriteLine($"You got an addition score of {score.additionScore} out of {question.additionQuestion}");
				Console.WriteLine($"You got a subtraction score of {score.subtractionScore} out of {question.subtractionQuestion}");
				Console.WriteLine($"You got a multiplication score of {score.multiplicationScore} out of {question.multiplicationQuestion}");
				Console.WriteLine($"You got a division score of {score.divisionScore} out of {question.divisionQuestion}");
			}
			else if (userDifficulty == UserDifficulty.Hard)
			{
				Console.WriteLine($"You got a multipication score of {score.multiplicationScore} out of {question.multiplicationQuestion}");
				Console.WriteLine($"You got a division score of {score.divisionScore} out of {question.divisionQuestion}");
				Console.WriteLine($"You got a power score of {score.powerScore} out of {question.powerQuestion}");
				Console.WriteLine($"You got a squareroot score of {score.squareRootScore} out of {question.squareRootQuestion}");
			}
		}
	}
}