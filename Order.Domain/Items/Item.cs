using System;
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
		public int StockAmount { get; private set; }
		public int Status { get; private set; }

		private static int CreateID;

		public Item(string name, string description, double price, int amount, int status)
		{
			ID = CreateID;
			Name = name;
			Description = description;
			Price = price;
			StockAmount = amount;
			Status = status;

			CreateID++;
		}

		public void UpdateAll(string itemName, string itemDescription, double itemPrice, int itemAmount)
		{
			Name = itemName;
			Description = itemDescription;
			Price = itemPrice;
			StockAmount = itemAmount;
		}

		public void UpdateName(string itemName)
		{
			Name = itemName;
		}

		public void UpdateDescription(string itemDescription)
		{
			Description = itemDescription;
		}

		public void UpdatePrice(double itemPrice)
		{
			Price = itemPrice;
		}

		public void UpdateAmount(int itemAmount)
		{
			StockAmount = itemAmount;
		}

		public void UpdateStatus(int itemStatus)
		{
			Status = itemStatus;
		}
	}
}
