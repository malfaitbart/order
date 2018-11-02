using Order.Domain.Items;
using Order.Domain.Users;
using System.Collections.Generic;

namespace Order.Data
{
	public static class Database
	{
		public static List<User> Users  = new List<User>()
			{
				new User("Bart", "Malfait", "malfaitbart@gmail.com", "0478513065", "Minister Alfred De Taeyestraat", "25", "8550", "Zwevegem")
			};
		public static Dictionary<int,string> UserRoles = new Dictionary<int, string>()
			{
				{ 1, "Customer"},
				{2, "Admin" }
			};
		public static Dictionary<int,string> UserStatus = new Dictionary<int, string>()
			{
				{1, "Active" },
				{2, "Disabled" }
			};
		public static List<Item> Items = new List<Item>()
		{
			new Item("Wit brood", "Wit melkbrood", 1.5, 5),
			new Item("Bruin brood", "Bruin brood met granen", 1.6, 3)
		};
	}
}
