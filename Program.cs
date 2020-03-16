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
                Console.Write("How many questions would you like to answer? Please type a number divisible by 10!");
                int.TryParse(Console.ReadLine(), out numberOfQuestions);
            } while (numberOfQuestions % 10 != 0);

            int numberOfQuestionsLeft = numberOfQuestions;
            Random random = new Random();
            int score = 0;
            int operationMin = 0;
            int operationMax = 0;

            switch (userDifficulty)
            {
                case "E":
                    operationMin = 1;
                    operationMax = 4;
                    break;
                case "N":
                    operationMin = 1;
                    operationMax = 5;
                    break;
                case "H":
                    operationMin = 3;
                    operationMax = 7;
                    break;
            }

            while (numberOfQuestionsLeft > 0)
            {
                var operation = random.Next(operationMin, operationMax); 
                double number1 = 0;
                double number2 = 0;
                double correctAnswer = 0;
                double userAnswer;

                string message = "";

                switch (operation)
                {
                    case 1:
                        number1 = random.Next(1000);
                        number2 = random.Next(1000);
                        correctAnswer = number1 + number2;
                        message = $"{number1} + {number2}";
                        break;
                    case 2:
                        number1 = random.Next(1000);
                        number2 = random.Next(1000);
                        correctAnswer = number1 - number2;
                        message = $"{number1} - {number2}";
                        break;
                    case 3:
                        if(userDifficulty == "E")
                        {
                            number1 = random.Next(13);
                            number2 = random.Next(13);
                        }
                        else
                        {
                            number1 = random.Next(1000);
                            number2 = random.Next(1000);
                        }
                        correctAnswer = number1 * number2;
                        message = $"{number1} * {number2}";
                        break;
                    case 4:
                        number1 = random.Next(1, 10000);
                        number2 = random.Next(1, 1000);
                        correctAnswer = number1 / number2;
                        message = $"{number1} / {number2}";
                        break;
                    case 5:
                        number1 = random.Next(13);
                        number2 = random.Next(5);
                        correctAnswer = Math.Pow(number1, number2);
                        message = $"{number1} ^ {number2}";
                        break;
                    case 6:
                        number1 = random.Next(1000);
                        correctAnswer = Math.Sqrt(number1);
                        message = $"√{number1}";
                        break;
                }


                if (operation == 4 || operation == 6)
                {
                    Console.Write($"To the nearest integer, What is {message} =");
                }
                else
                {
                    Console.Write($"What is {message} =");
                }

                userAnswer = Convert.ToDouble(Console.ReadLine());

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

            Console.WriteLine($"You got a score of {score} out of {numberOfQuestions}");
        }
    }
}