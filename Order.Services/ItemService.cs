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
			return Database.Items.FirstOrDefault(item => item.ID == id);
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
	}
}
