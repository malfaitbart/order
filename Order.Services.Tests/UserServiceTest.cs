using Order.Data;
using Order.Domain.Users;
using Order.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xunit;

namespace Order.Services.Tests
{
	public class UserServiceTest
	{
		[Fact]
		public void GivenAUserDatabase_WhenGetAll_ThenGetAListOfUsers()
		{
			//Given
			UserService userService = new UserService();
			//When
			var actual = userService.GetAll();
			//Then
			Assert.IsType<List<User>>(actual);
		}

		[Fact]
		public void GivenAUserDataBaseAndAUser_WhenAddUser_ThenUserIsAddedToDatabase()
		{
			//Given
			UserService userService = new UserService();
			
			User user = new User("test", "test", "test@test.com", "00000000", new Address("test", "test", "0000", "test"));
			//When
			userService.AddUser(user);
			//Then
			var actual = Database.Users.First(find => find.ID == user.ID);
			Assert.Equal(user, actual);
		}

		[Fact]
		public void GivenAUserDataBase_WhenGetByID_ThenUserIsReturned()
		{
			//Given
			UserService userService = new UserService();

			//When
			var actual = userService.GetUserByID(0);
			//Then
			Assert.Equal(0, actual.ID);
		}

		[Fact]
		public void GivenAUserDataBase_WhenGetByNonExistingID_ThenNullIsReturned()
		{
			//Given
			UserService userService = new UserService();

			//When
			var actual = userService.GetUserByID(-1);
			//Then
			Assert.Null(actual);
		}
	}
}
