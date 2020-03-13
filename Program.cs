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

            while (numberOfQuestionsLeft > 0)
            {
                var operation = random.Next(1, 7);
                int number1 = 0;
                int number2 = 0;
                switch (userDifficulty)
                {
                    case "E":
                        switch (operation)
                        {
                            case 1:
                                number1 = random.Next(1000);
                                number2 = random.Next(1000);
                                break;
                            case 2:
                                number1 = random.Next(1000);
                                number2 = random.Next(1000);
                                break;
                            case 3:
                                number1 = random.Next(13);
                                number2 = random.Next(13);
                                break;

                        }
                        break;
                    case "N":
                        switch (operation)
                        {
                            case 1:
                                number1 = random.Next(1000);
                                number2 = random.Next(1000);
                                break;
                            case 2:
                                number1 = random.Next(1000);
                                number2 = random.Next(1000);
                                break;
                            case 3:
                                number1 = random.Next(1000);
                                number2 = random.Next(1000);
                                break;
                            case 4:
                                number1 = random.Next(1, 10000);
                                number2 = random.Next(1, 1000);
                                break;

                        }
                        break;
                    case "H":
                        switch (operation)
                        {
                            case 3:
                                number1 = random.Next(1000);
                                number2 = random.Next(1000);
                                break;
                            case 4:
                                number1 = random.Next(1, 10000);
                                number2 = random.Next(1, 1000);
                                break;
                            case 5:
                                number1 = random.Next(13);
                                number2 = random.Next(5);
                                break;
                            case 6:
                                number1 = random.Next(1000);
                                break;

                        }
                        break;
                }

                if(operation == 1 && (userDifficulty == "E" || userDifficulty == "N"))
                {
                    Console.Write($"What is {number1} + {number2} =");
                    int correctAnswer = number1 + number2;
                    int userAnswer = Convert.ToInt32(Console.ReadLine());
                    if (correctAnswer == userAnswer)
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
                else if (operation == 2 && (userDifficulty == "E" || userDifficulty == "N"))
                {
                    Console.Write($"What is {number1} - {number2} =");
                    int correctAnswer = number1 - number2;
                    int userAnswer = Convert.ToInt32(Console.ReadLine());
                    if (correctAnswer == userAnswer)
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
                else if (operation == 3 && (userDifficulty == "E" || userDifficulty == "N" || userDifficulty == "H"))
                {
                    Console.Write($"What is {number1} * {number2} =");
                    int correctAnswer = number1 * number2;
                    int userAnswer = Convert.ToInt32(Console.ReadLine());
                    if (correctAnswer == userAnswer)
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
                else if (operation == 4 && (userDifficulty == "N" || userDifficulty == "H") && (number1 > number2))
                {
                    Console.Write($"To the nearest integer, What is {number1} / {number2} =");
                    double correctAnswer = number1 / number2;
                    double roundedCorrectAnser = Math.Round(correctAnswer);
                    int userAnswer = Convert.ToInt32(Console.ReadLine());
                    if (roundedCorrectAnser == userAnswer)
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
                else if (operation == 5 && userDifficulty == "H")
                {
                    Console.Write($"To the nearest integer, What is {number1} ^ {number2} =");
                    double correctAnswer = Math.Pow(number1, number2);
                    int userAnswer = Convert.ToInt32(Console.ReadLine());
                    if (correctAnswer == userAnswer)
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
                else if (operation == 6 && userDifficulty == "H")
                {
                    Console.Write($"To the nearest integer, What is √{number1} =");
                    double correctAnswer = Math.Sqrt(number1);
                    double roundedCorrectAnser = Math.Round(correctAnswer);
                    int userAnswer = Convert.ToInt32(Console.ReadLine());
                    if (roundedCorrectAnser == userAnswer)
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
            }
            Console.WriteLine($"You got a score of {score} out of {numberOfQuestions}");
        }
    }
}