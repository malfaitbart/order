using Order.Domain.Items;
using System;

namespace Order.Domain.Orders
{
	public class OrderItemGroup
	{
		public int ItemID { get; private set; }
		public string ItemName { get; private set; }
		public double ItemPrice { get; private set; }
		public int ItemAmount { get; private set; }
		public DateTime ShippingDate { get; private set; }
		public double ItemGroupTotalPrice { get => CalculateTotalItemGroupPrice(); }

		public OrderItemGroup(Item item, int amount)
		{
			ItemID = item.ID;
			ItemName = item.Name;
			ItemPrice = item.Price;
			ItemAmount = amount;
			ShippingDate = CheckStockToDecideShippIngDate(item, amount);
		}

		private DateTime CheckStockToDecideShippIngDate(Item item, int amount)
		{
			if (item.StockAmount > amount)
			{
				return DateTime.Now.Date;
			}
			return DateTime.Now.Date.AddDays(7);
		}

		private double CalculateTotalItemGroupPrice()
		{
			return ItemPrice * ItemAmount;
		}
	}
}