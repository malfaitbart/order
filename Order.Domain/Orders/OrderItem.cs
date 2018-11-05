using System;
using Order.Domain.Items;

namespace Order.Domain.Orders
{
	public class OrderItem
	{
		public int ItemID { get; private set; }
		public string ItemName { get; private set; }
		public double ItemPrice { get; private set; }
		public int Amount { get; private set; }
		public DateTime ShippingDate { get; private set; }
		public int OrderID { get; private set; }

		public OrderItem(Item item, int amount)
		{
			ItemID = item.ID;
			ItemName = item.Name;
			ItemPrice = item.Price;
			Amount = amount;
			ShippingDate = CheckStockToDecideShippIngDate(item, amount);
			OrderID = -1;
		}

		private DateTime CheckStockToDecideShippIngDate(Item item, int amount)
		{
			if (item.Amount > amount)
			{
				return DateTime.Now.Date;
			}
			return DateTime.Now.Date.AddDays(7);
		}

		public void SetOrderID(int orderid)
		{
			OrderID = orderid;
		}
	}
}