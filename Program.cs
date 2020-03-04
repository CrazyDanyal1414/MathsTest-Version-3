using System;

namespace mathstester
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("What difficulty level would you like to do! Please type E for Easy, N for Normal and H for hard");
            string userDifficulty = Console.ReadLine();
            if (userDifficulty == "E")
            {
                Console.Write("How many questions would you like to answer? ");
                int numberOfEasyQuestions = Convert.ToInt32(Console.ReadLine());
                int numberOfEasyQuestionsLeft = numberOfEasyQuestions;

                Random easyRandom = new Random();
                int easyNumber1 = easyRandom.Next(10);
                int easyNumber2 = easyRandom.Next(10);
                int easyScore = 0;

                while (numberOfEasyQuestionsLeft > 0)
                {
                    Console.Write($"What is {easyNumber1} * {easyNumber2} =");
                    int easyCorrectAnswer = easyNumber1 * easyNumber2;
                    int easyUserAnswer = Convert.ToInt32(Console.ReadLine());
                    if (easyCorrectAnswer == easyUserAnswer)
                    {
                        Console.WriteLine("Well Done!");
                        easyScore++;
                    }
                    else
                    {
                        Console.WriteLine("Your answer is incorrect!");
                    }
                    numberOfEasyQuestionsLeft--;
                    easyNumber1 = easyRandom.Next(10);
                    easyNumber2 = easyRandom.Next(10);
                }
                Console.WriteLine($"You got a score of {easyScore} out of {numberOfEasyQuestions}");
            }
            else if (userDifficulty == "N")
            {
                Console.Write("How many questions would you like to answer? ");
                int numberOfNormalQuestions = Convert.ToInt32(Console.ReadLine());
                int numberOfNormalQuestionsLeft = numberOfNormalQuestions;

                Random normalRandom = new Random();
                int normalNumber1 = normalRandom.Next(100);
                int normalNumber2 = normalRandom.Next(100);
                int normalScore = 0;

                while (numberOfNormalQuestionsLeft > 0)
                {
                    Console.Write($"What is {normalNumber1} * {normalNumber2} =");
                    int normalCorrectAnswer = normalNumber1 * normalNumber2;
                    int normalUserAnswer = Convert.ToInt32(Console.ReadLine());
                    if (normalCorrectAnswer == normalUserAnswer)
                    {
                        Console.WriteLine("Well Done!");
                        normalScore++;
                    }
                    else
                    {
                        Console.WriteLine("Your answer is incorrect!");
                    }
                    numberOfNormalQuestionsLeft--;
                    normalNumber1 = normalRandom.Next(100);
                    normalNumber2 = normalRandom.Next(100);
                }
                Console.WriteLine($"You got a score of {normalScore} out of {numberOfNormalQuestions}");
            }
            else if (userDifficulty == "H")
            {
                Console.Write("How many questions would you like to answer? ");
                int numberOfHardQuestions = Convert.ToInt32(Console.ReadLine());
                int numberOfHardQuestionsLeft = numberOfHardQuestions;

                Random hardRandom = new Random();
                int hardNumber1 = hardRandom.Next(10, 1000);
                int hardNumber2 = hardRandom.Next(10, 1000);
                int hardScore = 0;

                while (numberOfHardQuestionsLeft > 0)
                {
                    Console.Write($"What is {hardNumber1} * {hardNumber2} =");
                    int hardCorrectAnswer = hardNumber1 * hardNumber2;
                    int hardUserAnswer = Convert.ToInt32(Console.ReadLine());
                    if (hardCorrectAnswer == hardUserAnswer)
                    {
                        Console.WriteLine("Well Done!");
                        hardScore++;
                    }
                    else
                    {
                        Console.WriteLine("Your answer is incorrect!");
                    }
                    numberOfHardQuestionsLeft--;
                    hardNumber1 = hardRandom.Next(10, 1000);
                    hardNumber2 = hardRandom.Next(10, 1000);
                }
                Console.WriteLine($"You got a score of {hardScore} out of {numberOfHardQuestions}");
            }
            else
            {
                Console.WriteLine("Sorry, This is not an option");
            }
        }
    }
}