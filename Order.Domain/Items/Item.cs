﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Items
{
	public class Item
	{
		public int ID { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		public double Price { get; private set; }
		public int Amount { get; private set; }
		public int Status { get; private set; }

		private static int CreateID;

		public Item(string name, string description, double price, int amount, int status)
		{
			ID = CreateID;
			Name = name;
			Description = description;
			Price = price;
			Amount = amount;
			Status = status;

			CreateID++;
		}
	}
}
