using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace mathstester
{
	public class SaveLastTestResults
    {
		[Serializable]
		public class ToFile
		{
			public int TotalScore { get; private set; }
			public int NumberOfQuestions { get; }
			public UserDifficulty UserDifficulty { get; }
			public int TotalEasyQuestion { get; }
			public int TotalEasyScore { get; }
			public int TotalNormalQuestion { get; }
			public int TotalNormalScore { get; }
			public int TotalHardQuestion { get; }
			public int TotalHardScore { get; }
			public int EasyTests { get; }
			public int NormalTests { get; }
			public int HardTests { get; }

			public ToFile(int numberOfQuestions, UserDifficulty userDifficulty, int totalScore, int totalEasyQuestion, int totalEasyScore, int totalNormalQuestion, int totalNormalScore, int totalHardQuestion, int totalHardScore, int easyTests, int normalTests, int hardTests)
			{
				NumberOfQuestions = numberOfQuestions;
				UserDifficulty = userDifficulty;
				TotalScore = totalScore;
				TotalEasyQuestion = totalEasyQuestion;
				TotalEasyScore = totalEasyScore;
				TotalNormalQuestion = totalNormalQuestion;
				TotalNormalScore = totalNormalScore;
				TotalHardQuestion = totalHardQuestion;
				TotalHardScore = totalHardScore;
				EasyTests = easyTests;
				NormalTests = normalTests;
				HardTests = hardTests;
			}
		}

		public class SaveToFile
		{
			public static void SerializeLastTest(int numberOfQuestions, int totalScore, UserDifficulty userDifficulty, string userName, int totalEasyQuestion, int totalEasyScore, int totalNormalQuestion, int totalNormalScore, int totalHardQuestion, int totalHardScore, int easyTests, int normalTests, int hardTests)
			{
				ToFile obj = new ToFile(numberOfQuestions, userDifficulty, totalScore, totalEasyQuestion, totalEasyScore, totalNormalQuestion, totalNormalScore, totalHardQuestion, totalHardScore, easyTests, normalTests, hardTests);
				IFormatter formatter = new BinaryFormatter();
				Stream stream = new FileStream($"{userName}.txt", FileMode.Create, FileAccess.Write);
				formatter.Serialize(stream, obj);
				stream.Close();
			}
			public static ToFile DeserializeLastTest(string userName)
			{
				Stream stream = new FileStream($"{userName}.txt", FileMode.Open, FileAccess.Read);
				IFormatter formatter = new BinaryFormatter();
				ToFile objnew = (ToFile)formatter.Deserialize(stream);
				stream.Close();
				return objnew;
			}
		}
	}
}
