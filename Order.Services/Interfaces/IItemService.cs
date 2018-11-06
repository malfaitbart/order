using Order.Domain.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Services.Interfaces
{
	public interface IItemService
	{
		List<Item> GetAll();
		Item GetByID(int id);
		void AddItem(Item item);
		void UpdateItem(int itemID, string itemName, string itemDescription, double itemPrice, int itemAmount);
		List<Item> GetAllSortedByStock();
		List<Item> GetAllSortedByStock(string stockindicator);
	}
}
