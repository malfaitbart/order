using Order.Data;
using Order.Domain.Items;
using Order.Domain.Items.Exceptions;
using Order.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Order.Services
{
	public class ItemService : IItemService
	{
		public void AddItem(Item item)
		{
			Database.Items.Add(item);
		}

		public List<Item> GetAll()
		{
			return Database.Items;
		}

		public Item GetByID(int id)
		{
			return Database.Items.FirstOrDefault(item => item.ID == id && item.Status == 1);
		}

		public void UpdateItem(int itemID, string itemName, string itemDescription, double itemPrice, int itemAmount)
		{
			var item = GetByID(itemID);
			if(item == null)
			{
				throw new ItemException($"Item with id {itemID} does not exist. Update cancelled.");
			}
			Database.Items[itemID].UpdateAll(itemName, itemDescription, itemPrice, itemAmount);
		}

		public List<Item> GetAllSortedByStock()
		{
			return Database.Items.OrderBy(sort => sort.StockAmount).ToList();
		}

		public List<Item> GetAllSortedByStock(string stockindicator)
		{
			switch (stockindicator)
			{
				case "STOCK_LOW":
					return Database.Items.Where(item => item.StockAmount < 5).ToList();
				case "STOCK_MEDIUM":
					return Database.Items.Where(item => item.StockAmount < 10).ToList();
				case "STOCK_HIGH":
					return Database.Items.Where(item => item.StockAmount >= 10).ToList();
				default:
					throw new ItemException("Please use one of the following as query value: STOCK_LOW, STOCK_MEDIUM, STOCK_HIGH.\nOther values are not allowed.");

			}
		}
	}
}
