using System;

namespace mathstester
{
    class Program
    {
        enum userDifficulty
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
                int number1 = 0;
                int number2 = 0;
                switch (userDifficulty)
                {
                    case "E":
                        number1 = random.Next(10);
                        number2 = random.Next(10);
                        break;
                    case "N":
                        number1 = random.Next(100);
                        number2 = random.Next(100);
                        break;
                    case "H":
                        number1 = random.Next(10, 1000);
                        number2 = random.Next(10, 1000);
                        break;
                }

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
            Console.WriteLine($"You got a score of {score} out of {numberOfQuestions}");
        }
    }
}