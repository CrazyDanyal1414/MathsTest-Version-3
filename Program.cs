using System;

namespace mathstester
{
    class Program
    {
        public static void Main(string[] args)
        {

            Random rand = new Random();
            int number1 = rand.Next(11);
            int number2 = rand.Next(11);
            int mark = 0;


            Console.Write("How many questions would you like to answer? ");
            int numberOfQuestions = Convert.ToInt32(Console.ReadLine());
            int numberOfQuestionsLeft = numberOfQuestions;

            while (numberOfQuestionsLeft > 0)
            {

                Console.Write($"What is {number1} * {number2} =");
                int correctAnswer = number1 * number2;
                int userAnswer = Convert.ToInt32(Console.ReadLine());
                if (correctAnswer == userAnswer)
                {
                    Console.WriteLine("Well Done!");
                    mark++;
                }
                else
                {
                    Console.WriteLine("Your answer is incorrect!");
                    break;
                }
                numberOfQuestionsLeft--;
                number1 = rand.Next(21);
                number2 = rand.Next(21);
            }
            Console.WriteLine($"You got a score of {mark} out of {numberOfQuestions}");
        }
    }
}