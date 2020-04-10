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

            switch(userDifficulty)
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
			public int AdditionQuestion { get; set; }
			public int AdditionScore { get; set; }
			public int SubtractionQuestion { get; set; }
			public int SubtractionScore { get; set; }
			public int MultiplicationQuestion { get; set; }
			public int MultiplicationScore { get; set; }
			public int DivisionQuestion { get; set; }
			public int DivisionScore { get; set; }
			public int PowerQuestion { get; set; }
			public int PowerScore { get; set; }
			public int SquareRootQuestion { get; set; }
			public int SquareRootScore { get; set; }

            public int GetTotalScore()
            {
				int totalscore = 0;
				return totalscore++;
            }

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

		public static (OperationQuestionScore, OperationQuestionScore) RunTest(int numberOfQuestionsLeft, UserDifficulty userDifficulty)
		{
			Random random = new Random();
			var (operationMin, operationMax) = GetPossibleOperationsByDifficulty(userDifficulty);
			var score = new OperationQuestionScore();
			var totalScore = new OperationQuestionScore();
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
					totalScore.GetTotalScore();
					score.Increment(mathOperation, true);
				}
				else
				{
					Console.WriteLine("Your answer is incorrect!");
					score.Increment(mathOperation, false);
				}
				numberOfQuestionsLeft--;
			}
			return (totalScore, score);
		}
		public static void Main(string[] args)
		{
			Dictionary<string, UserDifficulty> difficultyDictionary = new Dictionary<string, UserDifficulty>();
			difficultyDictionary.Add("E", UserDifficulty.Easy);
			difficultyDictionary.Add("N", UserDifficulty.Normal);
			difficultyDictionary.Add("H", UserDifficulty.Hard);

			string userInputDifficulty;
			do
			{
				Console.WriteLine("What difficulty level would you like to do! Please type E for Easy, N for Normal and H for hard");
				userInputDifficulty = Console.ReadLine().ToUpper();
			} while (userInputDifficulty != "E" && userInputDifficulty != "N" && userInputDifficulty != "H");

			UserDifficulty userDifficulty = difficultyDictionary[userInputDifficulty];

			int numberOfQuestions;
			do
			{
				Console.WriteLine("How many questions would you like to answer? Please type a number divisible by 10!");
				int.TryParse(Console.ReadLine(), out numberOfQuestions);
			} while (numberOfQuestions % 10 != 0);

			var (totalScore, score) = RunTest(numberOfQuestions, userDifficulty);
			Console.WriteLine($"Total score: {totalScore} of {numberOfQuestions}");

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
		}
	}
}