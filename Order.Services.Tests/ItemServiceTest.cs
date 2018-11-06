using Order.Data;
using Order.Domain.Items;
using Order.Domain.Items.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Order.Services.Tests
{
	public class ItemServiceTest
	{
		[Fact]
		public void GivenAnItemDatabase_WhenGetAll_ThenGetAListOfItems()
		{
			//Given
			ItemService itemService = new ItemService();
			//When
			var actual = itemService.GetAll();
			//Then
			Assert.IsType<List<Item>>(actual);
		}

		[Fact]
		public void GivenAnItemDatabaseAndAnItem_WhenAddItem_ThenItemIsAddedToDatabase()
		{
			//Given
			ItemService itemService = new ItemService();
			Item item = new Item("test", "test", 5.0, 3, 1);
			//When
			itemService.AddItem(item);
			//Then
			var actual = Database.Items.First(find => find.ID == item.ID);
			Assert.Equal(item, actual);
		}

		[Fact]
		public void GivenAnItemDatabase_WhenGetByID_ThenItemWithThatIDIsReturned()
		{
			//Given
			ItemService itemService = new ItemService();
			Item item = new Item("test", "test", 5.0, 3, 1);
			itemService.AddItem(item);
			//When
			var actual = itemService.GetByID(item.ID);
			//Then
			Assert.Equal(item, actual);
		}
		[Fact]
		public void GivenAnItemDatabase_WhenGetByIDWithNonExistingID_ThenNullIsReturned()
		{
			//Given
			ItemService itemService = new ItemService();
			//When
			var actual = itemService.GetByID(-1);
			//Then
			Assert.Null(actual);
		}

		[Fact]
		public void GivenAnItemDatabaseAndAnItemService_WhenUpdateItem_ThenItemIsUpdatedInDB()
		{
			//Given
			ItemService itemService = new ItemService();

			//When
			itemService.UpdateItem(0, "testnaam", "testomschrijving", 4.3, 6);

			//Then
			Assert.Equal("testnaam", Database.Items[0].Name);
		}
		[Fact]
		public void GivenAnItemDatabaseAndAnItemService_WhenUpdateItemThatDoesNotExist_ThenExceptionisthrown()
		{
			//Given
			ItemService itemService = new ItemService();

			//When
			Action act = () => itemService.UpdateItem(-1, "testnaam", "testomschrijving", 4.3, 6);

			//Then
			var exception = Assert.Throws<ItemException>(act);
			Assert.Equal("Item with id -1 does not exist. Update cancelled.", exception.Message);
		}

	}
}
