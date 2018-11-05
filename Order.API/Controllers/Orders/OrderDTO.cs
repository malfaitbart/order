using System.Collections.Generic;

namespace Order.API.Controllers.Orders
{
	public class OrderDTO
	{
		public int ID { get;  set; }
		public List<OrderItemDTO> OrderItems { get;  set; }
		public double TotalPrice { get;  set; }
		public int CustomerID { get;  set; }

		public OrderDTO(int iD, List<OrderItemDTO> orderItems, double totalPrice, int customerID)
		{
			ID = iD;
			OrderItems = orderItems;
			TotalPrice = totalPrice;
			CustomerID = customerID;
		}
	}
}