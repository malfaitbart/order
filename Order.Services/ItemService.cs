using Order.Data;
using Order.Domain.Items;
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
	}
}
