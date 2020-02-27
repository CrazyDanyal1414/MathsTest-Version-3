using System;

namespace mathstest
{
    class Program
    {
        public static void Main(string[] args)
        {
            int mark = 0;

            Random rand0 = new Random();
            int num0 = rand0.Next(10);
            int num00 = rand0.Next(10);

            Console.WriteLine($"{num0} * {num00} =");
            int ans0 = num0 * num00;
            int a0 = Int32.Parse(Console.ReadLine());
            if (a0 == ans0)
            {
                Console.WriteLine("Well Done!");
                mark++;
            }
            else
            {
                Console.WriteLine($"Your answer is incorrect :( Answer = {ans0}");
            }

            Random rand1 = new Random();
            int num1 = rand1.Next(10);
            int num11 = rand1.Next(10);

            Console.WriteLine($"{num1} * {num11} =");
            int ans1 = num1 * num11;
            int a1 = Int32.Parse(Console.ReadLine());
            if (a1 == ans1)
            {
                Console.WriteLine("Well Done!");
                mark++;
            }
            else
            {
                Console.WriteLine($"Your answer is incorrect :( Answer = {ans1}");
            }

            Random rand2 = new Random();
            int num2 = rand2.Next(10);
            int num22 = rand2.Next(10);

            Console.WriteLine($"{num2} * {num22} =");
            int ans2 = num2 * num22;
            int a2 = Int32.Parse(Console.ReadLine());
            if (a2 == ans2)
            {
                Console.WriteLine("Well Done!");
                mark++;
            }
            else
            {
                Console.WriteLine($"Your answer is incorrect :( Answer = {ans2}");
            }

            Random rand3 = new Random();
            int num3 = rand3.Next(10);
            int num33 = rand3.Next(10);

            Console.WriteLine($"{num3} * {num33} =");
            int ans3 = num3 * num33;
            int a3 = Int32.Parse(Console.ReadLine());
            if (a3 == ans3)
            {
                Console.WriteLine("Well Done!");
                mark++;
            }
            else
            {
                Console.WriteLine($"Your answer is incorrect :( Answer = {ans3}");
            }

            Random rand4 = new Random();
            int num4 = rand4.Next(10);
            int num44 = rand4.Next(20);

            Console.WriteLine($"{num4} * {num44} =");
            int ans4 = num4 * num44;
            int a4 = Int32.Parse(Console.ReadLine());
            if (a4 == ans4)
            {
                Console.WriteLine("Well Done!");
                mark++;
            }
            else
            {
                Console.WriteLine($"Your answer is incorrect :( Answer = {ans4}");
            }

            Random rand5 = new Random();
            int num5 = rand5.Next(10);
            int num55 = rand5.Next(20);

            Console.WriteLine($"{num5} * {num55} =");
            int ans5 = num5 * num55;
            int a5 = Int32.Parse(Console.ReadLine());
            if (a5 == ans5)
            {
                Console.WriteLine("Well Done!");
                mark++;
            }
            else
            {
                Console.WriteLine($"Your answer is incorrect :( Answer = {ans5}");
            }

            Random rand6 = new Random();
            int num6 = rand6.Next(20);
            int num66 = rand6.Next(20);

            Console.WriteLine($"{num6} * {num66} =");
            int ans6 = num6 * num66;
            int a6 = Int32.Parse(Console.ReadLine());
            if (a6 == ans6)
            {
                Console.WriteLine("Well Done!");
                mark++;
            }
            else
            {
                Console.WriteLine($"Your answer is incorrect :( Answer = {ans6}");
            }

            Random rand7 = new Random();
            int num7 = rand7.Next(20);
            int num77 = rand7.Next(20);

            Console.WriteLine($"{num7} * {num77} =");
            int ans7 = num7 * num77;
            int a7 = Int32.Parse(Console.ReadLine());
            if (a7 == ans7)
            {
                Console.WriteLine("Well Done!");
                mark++;
            }
            else
            {
                Console.WriteLine($"Your answer is incorrect :( Answer = {ans7}");
            }

            Random rand8 = new Random();
            int num8 = rand8.Next(40);
            int num88 = rand8.Next(80);

            Console.WriteLine($"{num8} * {num88} =");
            int ans8 = num8 * num88;
            int a8 = Int32.Parse(Console.ReadLine());
            if (a8 == ans8)
            {
                Console.WriteLine("Well Done!");
                mark++;
            }
            else
            {
                Console.WriteLine($"Your answer is incorrect :( Answer = {ans8}");
            }

            Random rand9 = new Random();
            int num9 = rand9.Next(100);
            int num99 = rand9.Next(200);

            Console.WriteLine($"{num9} * {num99} =");
            int ans9 = num9 * num99;
            int a9 = Int32.Parse(Console.ReadLine());
            if (a9 == ans9)
            {
                Console.WriteLine("Well Done!");
                mark++;
            }
            else
            {
                Console.WriteLine($"Your answer is incorrect :( Answer = {ans9}");
            }

            Console.WriteLine($"You have a score of {mark} out of 10");

        }
    }
}
