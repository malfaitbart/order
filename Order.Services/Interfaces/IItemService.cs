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
	}
}
