using System.Collections.Generic;

namespace Order.API.Controllers.Orders
{
	public class OrderDTO
	{
		public int ID { get;  set; }
		public List<OrderItemGroupDTO> ItemGroup { get;  set; }
		public double TotalPrice { get;  set; }
		public int CustomerID { get;  set; }

		public OrderDTO(int iD, List<OrderItemGroupDTO> orderItems, double totalPrice, int customerID)
		{
			ID = iD;
			ItemGroup = orderItems;
			TotalPrice = totalPrice;
			CustomerID = customerID;
		}
	}
}