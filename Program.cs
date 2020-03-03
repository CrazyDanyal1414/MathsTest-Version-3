using System;

namespace mathstester
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("How many questions would you like to answer? ");
            int numberOfQuestions = Convert.ToInt32(Console.ReadLine());
            int numberOfQuestionsLeft = numberOfQuestions;

            Random random = new Random();
            int number1 = random.Next(11);
            int number2 = random.Next(11);
            int score = 0;

            while (numberOfQuestionsLeft > 0)
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
                number1 = random.Next(21);
                number2 = random.Next(21);
            }
            Console.WriteLine($"You got a score of {score} out of {numberOfQuestions}");
        }
    }
}