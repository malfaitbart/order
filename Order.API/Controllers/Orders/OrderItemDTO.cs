using System;

namespace Order.API.Controllers.Orders
{
	public class OrderItemDTO
	{
		public int ItemID { get;  set; }
		public string ItemName { get;  set; }
		public double ItemPrice { get;  set; }
		public int Amount { get;  set; }
		public DateTime ShippingDate { get;  set; }
		public int OrderID { get;  set; }

		public OrderItemDTO(int itemID, string itemName, double itemPrice, int amount, DateTime shippingDate, int orderID)
		{
			ItemID = itemID;
			ItemName = itemName;
			ItemPrice = itemPrice;
			Amount = amount;
			ShippingDate = shippingDate;
			OrderID = orderID;
		}
	}
}