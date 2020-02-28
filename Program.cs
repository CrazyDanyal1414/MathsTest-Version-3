using System;

namespace mathstester
{
    class Program
    {
        public static void Main(string[] args)
        {

            Random rand = new Random();
            int num1 = rand.Next(11);
            int num2 = rand.Next(11);
            int mark = 0;


            Console.Write("How many questions would you like to answer? ");
            int numofquestions = Convert.ToInt32(Console.ReadLine());
            int numofquestionsleft = numofquestions;

            // This is the loop which handles the actual question/answer core of the game.
            // Answering a question correctly increases your score.
            while (numofquestionsleft > 0)
            {

                Console.Write($"What is {num1} * {num2} =");
                int realanswer = num1 * num2;
                int useranswer = Convert.ToInt32(Console.ReadLine());
                if (realanswer == useranswer)
                {
                    Console.WriteLine("Well Done!");
                    mark++;
                }
                else
                {
                    Console.WriteLine("Your answer is incorrect!");
                }
                numofquestionsleft--;
                num1 = rand.Next(21);
                num2 = rand.Next(21);
            }
            Console.WriteLine($"You got a score of {mark} out of {numofquestions}");
        }
    }
}