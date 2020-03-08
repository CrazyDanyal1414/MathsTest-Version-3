using System;

namespace mathstester
{
    class Program
    {
        public static void Main(string[] args)
        {
            string userDifficulty = "";
            do
            {
                Console.WriteLine("What difficulty level would you like to do! Please type E for Easy, N for Normal and H for hard");
                userDifficulty = Console.ReadLine().ToUpper();
            }while (userDifficulty != "E" && userDifficulty != "N" && userDifficulty != "H");

            int numberOfQuestions = 0;
            int numberOfQuestionsLeft = 0;
            do
            {
                Console.Write("How many questions would you like to answer?Please type a number divisible by 10");
                int.TryParse(Console.ReadLine(), out numberOfQuestions);
                numberOfQuestionsLeft = numberOfQuestions;
            } while (numberOfQuestions % 10 != 0);

            Random random = new Random();
            int easyNumber1 = random.Next(10);
            int easyNumber2 = random.Next(10);
            int normalNumber1 = random.Next(100);
            int normalNumber2 = random.Next(100);
            int hardNumber1 = random.Next(10, 1000);
            int hardNumber2 = random.Next(10, 1000);
            int score = 0;

            while (numberOfQuestionsLeft > 0)
            {
                if (userDifficulty == "E")
                {
                    Console.Write($"What is {easyNumber1} * {easyNumber2} =");
                    int correctAnswer = easyNumber1 * easyNumber2;
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
                    easyNumber1 = random.Next(10);
                    easyNumber2 = random.Next(10);
                }
                else if (userDifficulty == "N")
                {
                    Console.Write($"What is {normalNumber1} * {normalNumber2} =");
                    int correctAnswer = normalNumber1 * normalNumber2;
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
                    normalNumber1 = random.Next(100);
                    normalNumber2 = random.Next(100);
                }
                else if (userDifficulty == "H")
                {
                    Console.Write($"What is {hardNumber1} * {hardNumber2} =");
                    int correctAnswer = hardNumber1 * hardNumber2;
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
                    hardNumber1 = random.Next(10, 1000);
                    hardNumber2 = random.Next(10, 1000);
                }
            }
            Console.WriteLine($"You got a score of {score} out of {numberOfQuestions}");
        }
    }
}