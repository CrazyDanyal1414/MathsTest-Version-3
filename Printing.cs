using System;

namespace mathstester
{
    public class Printing
    {
		public static string ReadInput()
		{
			string input = Console.ReadLine();
			Console.Write(new string(' ', 100));
			Console.CursorLeft = 0;
			return input;
		}

		static readonly object lockObject = new object();

		public static void WriteToScreen(string message, bool resetCursor)
		{
			lock (lockObject)
			{
				if (resetCursor)
				{
					int leftPos = Console.CursorLeft;
					Console.WriteLine();
					Console.Write(message.PadRight(50, ' '));
					Console.CursorTop--;
					Console.CursorLeft = leftPos;
				}
				else
				{
					Console.WriteLine(message);
					Console.Write(new string(' ', 100));
					Console.CursorLeft = 0;
				}
			}
		}
	}
}
