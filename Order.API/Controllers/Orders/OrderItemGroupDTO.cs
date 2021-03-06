﻿using System;

namespace Order.API.Controllers.Orders
{
	public class OrderItemGroupDTO
	{
		public int ItemID { get;  set; }
		public string ItemName { get;  set; }
		public double ItemPrice { get;  set; }
		public int Amount { get;  set; }
		public DateTime ShippingDate { get;  set; }
		public double ItemGroupTotalPrice { get; set; }

		public OrderItemGroupDTO(int itemID, string itemName, double itemPrice, int amount, DateTime shippingDate, double itemGroupTotalPrice)
		{
			ItemID = itemID;
			ItemName = itemName;
			ItemPrice = itemPrice;
			Amount = amount;
			ShippingDate = shippingDate;
			ItemGroupTotalPrice = itemGroupTotalPrice;
		}
	}
}