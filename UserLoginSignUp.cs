using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace mathstester
{
    public class LogInSignUp
    {
		[Serializable]
		public class User
		{
			public string UserName { get; set; }
			public string Password { get; set; }

			public User(string userName, string password)
			{
				UserName = userName;
				Password = password;
			}

			public override string ToString() => $"{UserName}, {Password}";
		}

		[Serializable]
		public class Users
		{
			public readonly List<User> Accounts;

			public Users() => Accounts = new List<User>();

			public void Save(string filePath)
			{
				if (string.IsNullOrEmpty(filePath)) return;

				var bf = new BinaryFormatter();
				using (var fs = new FileStream(filePath, FileMode.Create))
					bf.Serialize(fs, this);
			}

			public static Users Load(string filePath)
			{
				if (!File.Exists(filePath)) return null;

				var bf = new BinaryFormatter();
				using (var sr = new FileStream(filePath, FileMode.Open))
					return bf.Deserialize(sr) as Users;
			}

			public bool ContainsUserName(string userName) =>
				Accounts.Any(x => x.UserName == userName);

			public bool ContainsAccount(string userName, string pass) =>
				Accounts.Any(x => x.UserName == userName && x.Password == pass);

			public User Get(string userName, string pass) =>
				Accounts.FirstOrDefault(x => x.UserName == userName && x.Password == pass);

			public bool Add(string userName, string pass)
			{
				if (ContainsUserName(userName)) return false;

				Accounts.Add(new User(userName, pass));
				return true;
			}
		}
	}
}
