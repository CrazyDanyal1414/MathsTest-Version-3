using System;

namespace mathstester
{
    class Program
    {
		enum UserDifficulty
		{
			Easy,
			Normal,
			Hard
		}

		enum MathOperation
		{
			Addition = 1,
			Subtraction = 2,
			Multiplication = 3,
			Division = 4,
			Power = 5,
			SquareRoot = 6
		}

		public static (int operationMin, int operationMax) GetDifficulty(string input)
		{
			switch (input)
			{
				case "E":
					return (1, 4);
				case "N":
					return (1, 5);
				case "H":
					return (3, 7);
				default:
					throw new Exception();
			}
		}

		public static (string message, double correctAnswer) Calculation(int mathOperation, string userDifficulty)
		{
			int number1;
			int number2;
			Random random = new Random();

			switch (mathOperation)
			{
				case 1:
					number1 = random.Next(1000);
					number2 = random.Next(1000);
					return ($"{number1} + {number2}", number1 + number2);
				case 2:
					number1 = random.Next(1000);
					number2 = random.Next(1000);
					return ($"{number1} - {number2}", number1 - number2);
				case 3:
					number1 = userDifficulty == "E" ? random.Next(13) : random.Next(1000);
					number2 = userDifficulty == "E" ? random.Next(13) : random.Next(1000);
					return ($"{number1} * {number2}", number1 * number2);
				case 4:
					number1 = random.Next(10000);
					number2 = random.Next(1000);
					return ($"{number1} / {number2}", number1 / (double)number2);
				case 5:
					number1 = random.Next(13);
					number2 = random.Next(5);
					return ($"{number1} ^ {number2}", Math.Pow(number1, number2));
				case 6:
					number1 = random.Next(1000);
					return ($"√{number1}", Math.Sqrt(number1));
				default:
					throw new Exception();
			}
		}

		public static int GetResult(int numberOfQuestionsLeft, string userDifficulty)
		{
			int score = 0;
			Random random = new Random();
			var (operationMin, operationMax) = GetDifficulty(userDifficulty);
			while (numberOfQuestionsLeft > 0)
			{
				int mathOperation = random.Next(operationMin, operationMax);
				var (message, correctAnswer) = Calculation(mathOperation, userDifficulty);
				if (mathOperation == 4 || mathOperation == 6)
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
					score++;
				}
				else
				{
					Console.WriteLine("Your answer is incorrect!");
				}
				numberOfQuestionsLeft--;
			}

			return score;
		}

		public static void Main(string[] args)
		{
			string userDifficulty = "";
			do
			{
				Console.WriteLine("What difficulty level would you like to do! Please type E for Easy, N for Normal and H for hard");
				userDifficulty = Console.ReadLine().ToUpper();
			} while (userDifficulty != "E" && userDifficulty != "N" && userDifficulty != "H");

			int numberOfQuestions = 0;
			do
			{
				Console.WriteLine("How many questions would you like to answer? Please type a number divisible by 10!");
				int.TryParse(Console.ReadLine(), out numberOfQuestions);
			} while (numberOfQuestions % 10 != 0);

			int score = GetResult(numberOfQuestions, userDifficulty);
			Console.WriteLine($"You got a score of {score} out of {numberOfQuestions}");
		}
	}
}