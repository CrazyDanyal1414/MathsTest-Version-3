using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace mathstester
{
	public class SaveLastTest
    {
		[Serializable]
		public class ToFile
		{
			public int TotalScore { get; private set; }
			public int NumberOfQuestions { get; }
			public UserDifficulty UserDifficulty { get; }
			public ToFile(int numberOfQuestions, UserDifficulty userDifficulty, int totalScore)
			{
				NumberOfQuestions = numberOfQuestions;
				UserDifficulty = userDifficulty;
				TotalScore = totalScore;
			}
		}

		public class SaveToFile
		{
			public static void Serialize(int numberOfQuestions, int totalScore, UserDifficulty userDifficulty)
			{
				ToFile obj = new ToFile(numberOfQuestions, userDifficulty, totalScore);
				IFormatter formatter = new BinaryFormatter();
				Stream stream = new FileStream("D:\\Example.txt", FileMode.Create, FileAccess.Write);
				formatter.Serialize(stream, obj);
				stream.Close();
			}
			public static ToFile Deserialize()
			{
				Stream stream = new FileStream("D:\\Example.txt", FileMode.Open, FileAccess.Read);
				IFormatter formatter = new BinaryFormatter();
				ToFile objnew = (ToFile)formatter.Deserialize(stream);
				stream.Close();
				return objnew;
			}
		}

		public static UserDifficulty SuggestingDifficulty()
		{
			ToFile objnew = SaveToFile.Deserialize();
			UserDifficulty userDifficulty = UserDifficulty.Easy;

			Console.WriteLine($"Last time you did the test on {objnew.UserDifficulty} level and got {objnew.TotalScore}/{objnew.NumberOfQuestions}");
			double decimalScore = (double)objnew.TotalScore / (double)objnew.NumberOfQuestions;

			if (objnew.UserDifficulty == UserDifficulty.Easy)
			{
				if (decimalScore <= 0.7)
				{
					Console.WriteLine($"You should stay on Easy difficulty");
					userDifficulty = UserDifficulty.Easy;
				}
				else
				{
					Console.WriteLine($"Easy difficulty seems to easy for you💪! You should go up to Normal difficulty");
					userDifficulty = UserDifficulty.Normal;
				}
			}
			else if (objnew.UserDifficulty == UserDifficulty.Normal)
			{
				if (decimalScore <= 0.3)
				{
					Console.WriteLine($"Normal difficulty seems to be to hard for you☹️. You should go down to Easy difficulty");
					userDifficulty = UserDifficulty.Easy;
				}
				else if ((decimalScore > 0.3) && (decimalScore <= 0.7))
				{
					Console.WriteLine($"You should stay on Normal difficulty");
					userDifficulty = UserDifficulty.Normal;
				}
				else
				{
					Console.WriteLine($"Normal difficulty seems to easy for you💪! You should go up to Hard difficulty");
					userDifficulty = UserDifficulty.Hard;
				}
			}
			else if (objnew.UserDifficulty == UserDifficulty.Hard)
			{
				if (decimalScore <= 0.3)
				{
					Console.WriteLine($"Hard difficulty seems to hard for you☹️. You should go down to Normal difficulty");
					userDifficulty = UserDifficulty.Normal;
				}
				else if ((decimalScore > 0.3) && (decimalScore <= 0.8))
				{
					Console.WriteLine($"You should stay on Hard difficulty");
					userDifficulty = UserDifficulty.Hard;
				}
				else
				{
					Console.WriteLine($"You are a maths Genius🥳! Sadly this is the hardest level");
					userDifficulty = UserDifficulty.Hard;
				}
			}
			return userDifficulty;
		}
	}
}
