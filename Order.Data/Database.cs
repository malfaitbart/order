using Order.Domain.Items;
using Order.Domain.Orders;
using Order.Domain.Users;
using System;
using System.Collections.Generic;

namespace Order.Data
{
	public static class Database
	{
		public static Dictionary<int, string> UserRoles = new Dictionary<int, string>()
			{
				{1, "Customer"},
				{2, "Admin" }
			};

		public static Dictionary<int, string> UserStatus = new Dictionary<int, string>()
			{
				{1, "Active" },
				{2, "Disabled" }
			};

		public static Dictionary<int, string> ItemStatus = new Dictionary<int, string>()
			{
				{1, "Active" },
				{2, "Non-Active" }
			};

		public static List<User> Users = new List<User>()
			{
				new User("Bart", "Malfait", "malfaitbart@gmail.com", "0478513065", new Address("Minister Alfred De Taeyestraat", "25", "8550", "Zwevegem")),
				new User("Admin", "Admin", "admin@localhost.be", "0000000", new Address("street", "0", "0", "0"), 2)
			};

		public static List<Item> Items = new List<Item>()
		{
			new Item("Wit brood", "Wit melkbrood", 1.5, 10, 1),
			new Item("Bruin brood", "Bruin brood met granen", 1.6, 7, 1),
			new Item("Nutella", "smeerpasta met chocolade", 2.0, 25, 1),
			new Item("Melk", "Pack van 6x halve liter", 1.8, 3, 1)
		};

		public static List<Domain.Orders.Order> Orders = new List<Domain.Orders.Order>()
		{
			new Order.Domain.Orders.Order(new List<OrderItemGroup>(){ new OrderItemGroup(Items[0], 3), new OrderItemGroup(Items[1], 3) }, 0)
		};
	}
}
