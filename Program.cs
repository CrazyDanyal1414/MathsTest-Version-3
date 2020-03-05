using System;

namespace mathstester
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("What difficulty level would you like to do! Please type E for Easy, N for Normal and H for hard");
            string userDifficulty = Console.ReadLine();

            Console.Write("How many questions would you like to answer? ");
            int numberOfQuestions = Convert.ToInt32(Console.ReadLine());
            int numberOfQuestionsLeft = numberOfQuestions;

            Random random = new Random();
            int score = 0;

            while (numberOfQuestionsLeft > 0)
            {
                if (userDifficulty == "E")
                {
                    int easyNumber1 = random.Next(10);
                    int easyNumber2 = random.Next(10);

                    Console.Write($"What is {easyNumber1} * {easyNumber2} =");
                    int easyCorrectAnswer = easyNumber1 * easyNumber2;
                    int easyUserAnswer = Convert.ToInt32(Console.ReadLine());
                    if (easyCorrectAnswer == easyUserAnswer)
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
                    int normalNumber1 = random.Next(100);
                    int normalNumber2 = random.Next(100);

                    Console.Write($"What is {normalNumber1} * {normalNumber2} =");
                    int normalCorrectAnswer = normalNumber1 * normalNumber2;
                    int normalUserAnswer = Convert.ToInt32(Console.ReadLine());
                    if (normalCorrectAnswer == normalUserAnswer)
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
                    int hardNumber1 = random.Next(10, 1000);
                    int hardNumber2 = random.Next(10, 1000);
                    Console.Write($"What is {hardNumber1} * {hardNumber2} =");
                    int hardCorrectAnswer = hardNumber1 * hardNumber2;
                    int hardUserAnswer = Convert.ToInt32(Console.ReadLine());
                    if (hardCorrectAnswer == hardUserAnswer)
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
                else
                {
                    Console.WriteLine("Sorry, This is not an option");
                }
            }
            Console.WriteLine($"You got a score of {score} out of {numberOfQuestions}");
        }
    }
}