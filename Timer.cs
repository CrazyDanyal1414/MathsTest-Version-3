using System;
using System.Threading;

namespace mathstester
{
    public class RunWithTimer
    {
		public bool IsTimeLeft { get; private set; } = true;
		public static void Timer(int numberOfSeconds)
		{
			var whenToStop = DateTime.Now.AddSeconds(numberOfSeconds);
			while (DateTime.Now < whenToStop)
			{
				string timeLeft = (whenToStop - DateTime.Now).ToString(@"hh\:mm\:ss");
				WriteToScreen($"Time Remaining: {timeLeft}", true);
				Thread.Sleep(1000);
			}
		}

		public Thread timerThread;
		public RunWithTimer(int numberOfSeconds)
		{
			timerThread = new Thread(new ThreadStart(() =>
			{
				Timer(numberOfSeconds);
				timerThread = null;
				IsTimeLeft = false;
			}));
			timerThread.Start();
		}
		public void StopTimer(int numberOfQuestionsLeft)
		{
			if (numberOfQuestionsLeft == 0)
			{
				timerThread.Abort();
			}
		}
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
