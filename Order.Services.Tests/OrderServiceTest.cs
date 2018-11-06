using Order.Data;
using Order.Domain.Items;
using Order.Domain.Orders;
using Order.Domain.Orders.Exceptions;
using Order.Services.Interfaces;
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
			ItemService itemService = new ItemService();
			OrderService orderService = new OrderService(itemService);
			
			List<IncomingOrderItemGroup> itemGroup= new List<IncomingOrderItemGroup>
			{
				new IncomingOrderItemGroup(Database.Items[0].ID, 1),
				new IncomingOrderItemGroup(Database.Items[1].ID, 2)
			};
			//When
			var orderid = orderService.CreateOrder(Database.Users[0].ID, itemGroup);

			//Then
			var actual = Database.Orders.FirstOrDefault(order => order.ID == orderid);
			Assert.Equal(orderid, actual.ID);
		}
		[Fact]
		public void GivenAnOrderServiceWhenOrderingNonExistingItems_ThenGetOrderException()
		{
			//Given
			ItemService itemService = new ItemService();

			OrderService orderService = new OrderService(itemService);

			var user = Database.Users[0];
			List<IncomingOrderItemGroup> itemGroup = new List<IncomingOrderItemGroup>
			{
				new IncomingOrderItemGroup(-1,2),
			};
			//When
			Action act = () => orderService.CreateOrder(user.ID, itemGroup);

			//Then
			var exception = Assert.Throws<OrderException>(act);
			Assert.Equal($"The item with id -1 does not exist.", exception.Message);
		}
		[Fact]
		public void GivenAnOrderService_WhenGetAll_ThenGetListOdOrders()
		{
			//Given
			ItemService itemService = new ItemService();

			OrderService orderService = new OrderService(itemService);
			//When
			var actual = orderService.GetAll();

			//Then
			Assert.IsType<List<Domain.Orders.Order>>(actual);
		}
		[Fact]
		public void GivenAnOrderService_WhenGetByExistingId_ThenGetOrder()
		{
			//Given
			ItemService itemService = new ItemService();

			OrderService orderService = new OrderService(itemService);

			var user = Database.Users[0];

			List<IncomingOrderItemGroup> itemGroup = new List<IncomingOrderItemGroup>
			{
				new IncomingOrderItemGroup(Database.Items[0].ID, 1),
				new IncomingOrderItemGroup(Database.Items[1].ID, 2)
			};
			orderService.CreateOrder(user.ID, itemGroup);
			//When
			var actual = orderService.GetByID(0);

			//Then
			Assert.Equal(0, actual.ID);
		}
		[Fact]
		public void GivenAnOrderService_WhenGetByNonExistingId_ThenGetOrder()
		{
			//Given
			ItemService itemService = new ItemService();

			OrderService orderService = new OrderService(itemService);
			//When
			var actual = orderService.GetByID(-1);

			//Then
			Assert.Null(actual);
		}

	}
}
