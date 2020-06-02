using System;
using System.Threading;
using static mathstester.UseManyTimes;

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
	}
}
