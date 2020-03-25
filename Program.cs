using System;

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

		public static int GetResult(int numberOfQuestionsLeft, UserDifficulty userDifficulty)
		{
			int score = 0;
			Random random = new Random();
			var (operationMin, operationMax) = GetPossibleOperationsByDifficulty(userDifficulty);
			while (numberOfQuestionsLeft > 0)
			{
				MathOperation mathOperation = MathOperation.Addition;
                int mathRandomOperation = random.Next(operationMin, operationMax);

                switch (mathRandomOperation)
                {
					case 1:
						mathOperation = MathOperation.Addition;
						break;
					case 2:
						mathOperation = MathOperation.Subtraction;
						break;
					case 3:
						mathOperation = MathOperation.Multiplication;
						break;
					case 4:
						mathOperation = MathOperation.Division;
						break;
					case 5:
						mathOperation = MathOperation.Power;
						break;
					case 6:
						mathOperation = MathOperation.SquareRoot;
						break;
				}

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
			string userInputDifficulty = "";
			UserDifficulty userDifficulty = UserDifficulty.Easy;
			do
			{
				Console.WriteLine("What difficulty level would you like to do! Please type E for Easy, N for Normal and H for hard");
				userInputDifficulty = Console.ReadLine().ToUpper();
			} while (userInputDifficulty != "E" && userInputDifficulty != "N" && userInputDifficulty != "H");

			switch (userInputDifficulty)
			{
				case "E":
					userDifficulty = UserDifficulty.Easy;
					break;
				case "N":
					userDifficulty = UserDifficulty.Normal;
					break;
				case "H":
					userDifficulty = UserDifficulty.Hard;
					break;
			}

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