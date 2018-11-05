using Order.Data;
using Order.Domain.Items;
using Order.Domain.Orders;
using Order.Domain.Orders.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Order.Services.Tests
{
	public class OrderServiceTest
	{
		[Fact]
		public void GivenAnOrderServiceAndItems_WhenOrdering_TheOrderIsCreated()
		{
			//Given
			OrderService orderService = new OrderService();
			
			List<OrderItem> orderItems = new List<OrderItem>
			{
				new OrderItem(Database.Items[0], 1),
				new OrderItem(Database.Items[1], 2)
			};
			//When
			var orderid = orderService.CreateOrder(Database.Users[0].ID, orderItems);

			//Then
			var actual = Database.Orders.FirstOrDefault(order => order.ID == orderid);
			Assert.Equal(orderid, actual.ID);
		}
		[Fact]
		public void GivenAnOrderServiceWhenOrderingNonExistingItems_ThenGetOrderException()
		{
			//Given
			OrderService orderService = new OrderService();

			var user = Database.Users[0];
			var item = new Item("test", "zever", 10.2, 3, 1);
			List<OrderItem> orderItems = new List<OrderItem>
			{
				new OrderItem(item,2),
			};
			//When
			Action act = () => orderService.CreateOrder(user.ID, orderItems);

			//Then
			var exception = Assert.Throws<OrderException>(act);
			Assert.Equal($"The item with id {item.ID} does not exist.", exception.Message);
		}
		[Fact]
		public void GivenAnOrderService_WhenGetAll_ThenGetListOdOrders()
		{
			//Given
			OrderService orderService = new OrderService();
			//When
			var actual = orderService.GetAll();

			//Then
			Assert.IsType<List<Domain.Orders.Order>>(actual);
		}
		[Fact]
		public void GivenAnOrderService_WhenGetByExistingId_ThenGetOrder()
		{
			//Given
			OrderService orderService = new OrderService();

			var item1 = Database.Items[0];
			var amount1 = 1;
			var item2 = Database.Items[1];
			var amount2 = 2;

			var user = Database.Users[0];

			List<OrderItem> orderItems = new List<OrderItem>
			{
				new OrderItem(item1, amount1),
				new OrderItem(item2, amount2)
			};
			orderService.CreateOrder(user.ID, orderItems);
			//When
			var actual = orderService.GetByID(0);

			//Then
			Assert.Equal(0, actual.ID);
		}
		[Fact]
		public void GivenAnOrderService_WhenGetByNonExistingId_ThenGetOrder()
		{
			//Given
			OrderService orderService = new OrderService();
			//When
			var actual = orderService.GetByID(-1);

			//Then
			Assert.Null(actual);
		}

	}
}
