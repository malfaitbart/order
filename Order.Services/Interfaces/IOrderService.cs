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
		int CreateOrder(int userID, List<OrderItem> orderItems);
		Domain.Orders.Order GetByID(int id);
	}
}
