using System;
using System.Collections.Generic;
using System.IO;
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
			public ToFile(int numberOfQuestions, UserDifficulty userDifficulty, int totalScore)
			{
				NumberOfQuestions = numberOfQuestions;
				UserDifficulty = userDifficulty;
				TotalScore = totalScore;
			}
		}

		[Serializable]
		public class Users
		{
			public string UserName { get; set; }
			public string Password { get; set; }

			public Users(string userName, string password)
			{
				UserName = userName;
				Password = password;
			}

			public override string ToString() => $"{UserName}, {Password}";
		}

		public class SaveToFile
		{
			public static void SerializeLastTest(int numberOfQuestions, int totalScore, UserDifficulty userDifficulty)
			{
				ToFile obj = new ToFile(numberOfQuestions, userDifficulty, totalScore);
				IFormatter formatter = new BinaryFormatter();
				Stream stream = new FileStream("LastTest.txt", FileMode.Create, FileAccess.Write);
				formatter.Serialize(stream, obj);
				stream.Close();
			}
			public static ToFile DeserializeLastTest()
			{
				Stream stream = new FileStream("LastTest.txt", FileMode.Open, FileAccess.Read);
				IFormatter formatter = new BinaryFormatter();
				ToFile objnew = (ToFile)formatter.Deserialize(stream);
				stream.Close();
				return objnew;
			}
			public static void SerializeSignUpDetails(string userName, string password)
			{
				Users obj = new Users(userName, password);
				IFormatter formatter = new BinaryFormatter();
				Stream stream = new FileStream("SignUp.txt", FileMode.Append, FileAccess.Write);
				formatter.Serialize(stream, obj);
				stream.Close();
			}
			public static List<Users> DeserializeSignUpDetails()
			{
				List<Users> users = new List<Users>();

				using (Stream stream = new FileStream("SignUp.txt", FileMode.Open, FileAccess.Read))
				{
					if (stream.Length != 0)
					{
						IFormatter formatter = new BinaryFormatter();
						while (stream.Position != stream.Length)
						{
							users.Add((Users)formatter.Deserialize(stream));
						}
						return users;
					}
				}
				return users;
			}
		}
	}
}
