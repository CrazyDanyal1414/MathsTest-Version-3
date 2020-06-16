using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace mathstester
{
    public class UserStatistics
    {
		[Serializable]
		public class Stats
		{
			public int TotalEasyQuestion { get; }
			public int TotalEasyScore { get; }
			public int TotalNormalQuestion { get; }
			public int TotalNormalScore { get; }
			public int TotalHardQuestion { get; }
			public int TotalHardScore { get; }
			public Stats(int totalEasyQuestion, int totalEasyScore, int totalNormalQuestion, int totalNormalScore, int totalHardQuestion, int totalHardScore)
			{
				TotalEasyQuestion = totalEasyQuestion;
				TotalEasyScore = totalEasyScore;
				TotalNormalQuestion = totalNormalQuestion;
				TotalNormalScore = totalNormalScore;
				TotalHardQuestion = totalHardQuestion;
				TotalHardScore = totalHardScore;
			}
		}
		public class SaveStatsToFile
		{
			public static void SerializeStats(int userName, int totalEasyQuestion, int totalEasyScore, int totalNormalQuestion, int totalNormalScore, int totalHardQuestion, int totalHardScore)
			{
				Stats obj = new Stats(totalEasyQuestion, totalEasyScore, totalNormalQuestion, totalNormalScore, totalHardQuestion, totalHardScore);
				IFormatter formatter = new BinaryFormatter();
				Stream stream = new FileStream($"{userName}.txt", FileMode.Create, FileAccess.Write);
				formatter.Serialize(stream, obj);
				stream.Close();
			}
			public static Stats DeserializeStats(string userName)
			{
				Stream stream = new FileStream($"{userName}.txt", FileMode.Open, FileAccess.Read);
				IFormatter formatter = new BinaryFormatter();
				Stats objnew = (Stats)formatter.Deserialize(stream);
				stream.Close();
				return objnew;
			}
		}
	}
}
