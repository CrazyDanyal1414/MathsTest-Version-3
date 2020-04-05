using System;
using System.Collections.Generic;
using System.Linq;
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

			public static (Action<OperationQuestionScore>, Action<OperationQuestionScore>) Scores(MathOperation mathOperation)
			{
				Action<OperationQuestionScore> incrementOperationQuestion;
				Action<OperationQuestionScore> incrementOperationScore;
				switch (mathOperation)
				{
                    case MathOperation.Addition:
						incrementOperationQuestion = incrementquestion => incrementquestion.AdditionQuestion++;
						incrementOperationScore = incrementscore => incrementscore.AdditionScore++;
						break;
					case MathOperation.Subtraction:
						incrementOperationQuestion = incrementquestion => incrementquestion.SubtractionQuestion++;
						incrementOperationScore = incrementscore => incrementscore.SubtractionScore++;
						break;
					case MathOperation.Multiplication:
						incrementOperationQuestion = incrementquestion => incrementquestion.MultiplicationQuestion++;
						incrementOperationScore = incrementscore => incrementscore.MultiplicationScore++;
						break;
					case MathOperation.Division:
						incrementOperationQuestion = incrementquestion => incrementquestion.DivisionQuestion++;
						incrementOperationScore = incrementscore => incrementscore.DivisionScore++;
						break;
					case MathOperation.Power:
						incrementOperationQuestion = incrementquestion => incrementquestion.PowerQuestion++;
						incrementOperationScore = incrementscore => incrementscore.PowerScore++;
						break;
					case MathOperation.SquareRoot:
						incrementOperationQuestion = incrementquestion => incrementquestion.SquareRootQuestion++;
						incrementOperationScore = incrementscore => incrementscore.SquareRootScore++;
						break;
					default:
						throw new Exception();
				}
				return (incrementOperationQuestion, incrementOperationScore);
			}
		}

		public static OperationQuestionScore Score()
		{
			return new OperationQuestionScore();
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

				var (incrementOperationQuestion, incrementOperationScore) = OperationQuestionScore.Scores(mathOperation);

				double userAnswer = Convert.ToDouble(Console.ReadLine());
				if (Math.Round(correctAnswer) == userAnswer)
				{
					Console.WriteLine("Well Done!");
					incrementOperationQuestion(question);
					incrementOperationScore(score);
					totalScore++;
				}
				else
				{
					Console.WriteLine("Your answer is incorrect!");
					incrementOperationQuestion(question);
				}
				numberOfQuestionsLeft--;
			}
			return (totalScore, score, question);
		}
		public static void Main(string[] args)
		{
			Dictionary<string, UserDifficulty> difficultyDictionary = new Dictionary<string, UserDifficulty>();
			difficultyDictionary.Add("E", UserDifficulty.Easy);
			difficultyDictionary.Add("N", UserDifficulty.Normal);
			difficultyDictionary.Add("H", UserDifficulty.Hard);

			string userInputDifficulty = "";
			do
			{
				Console.WriteLine("What difficulty level would you like to do! Please type E for Easy, N for Normal and H for hard");
				userInputDifficulty = Console.ReadLine().ToUpper();
			} while (userInputDifficulty != "E" && userInputDifficulty != "N" && userInputDifficulty != "H");

			UserDifficulty userDifficulty = difficultyDictionary[userInputDifficulty];

			int numberOfQuestions = 0;
			do
			{
				Console.WriteLine("How many questions would you like to answer? Please type a number divisible by 10!");
				int.TryParse(Console.ReadLine(), out numberOfQuestions);
			} while (numberOfQuestions % 10 != 0);

			var (totalScore, score, question) = RunTest(numberOfQuestions, userDifficulty);
			Console.WriteLine($"Total score: {totalScore} of {numberOfQuestions}");

			if (userDifficulty == UserDifficulty.Easy)
			{
				Console.WriteLine($"Addition score: {score.AdditionScore} of {question.AdditionQuestion}");
				Console.WriteLine($"Subtraction score: {score.SubtractionScore} of {question.SubtractionQuestion}");
				Console.WriteLine($"Multiplication score: {score.MultiplicationScore} of {question.MultiplicationQuestion}");
			}
			else if (userDifficulty == UserDifficulty.Normal)
			{
				Console.WriteLine($"Addition score: {score.AdditionScore} of {question.AdditionQuestion}");
				Console.WriteLine($"Subtraction score: {score.SubtractionScore} of {question.SubtractionQuestion}");
				Console.WriteLine($"Multiplication score: {score.MultiplicationScore} of {question.MultiplicationQuestion}");
				Console.WriteLine($"Division score: {score.DivisionScore} of {question.DivisionQuestion}");
			}
			else if (userDifficulty == UserDifficulty.Hard)
			{
				Console.WriteLine($"Multipication score: {score.MultiplicationScore} of {question.MultiplicationQuestion}");
				Console.WriteLine($"Division score: {score.DivisionScore} of {question.DivisionQuestion}");
				Console.WriteLine($"Power score: {score.PowerScore} of {question.PowerQuestion}");
				Console.WriteLine($"Squareroot score: {score.SquareRootScore} of {question.SquareRootQuestion}");
			}
		}
	}
}