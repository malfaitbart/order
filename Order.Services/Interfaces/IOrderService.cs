using Order.Domain.Items;
using Order.Domain.Orders;
using Order.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;
namespace Order.Services.Interfaces
{
	public interface IOrderService
	{
		List<Domain.Orders.Order> GetAll();
		int CreateOrder(int customerID, List<IncomingOrderItemGroup> incomingOrderItems);
		Domain.Orders.Order GetByID(int id);
		Tuple<List<Domain.Orders.Order>, double> GetOrdersReport(int customerID);
		int ReOrder(int orderID, int customerID);
		List<Domain.Orders.Order> GetOrdersWithItemGroupsShipping(uint todayPlusShippingday);
	}
}
