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
		public void GivenAUserDataBaseAndAUser_WhenAddUser_ThenUserIsAddedToDataSet()
		{
			//Given
			UserService userService = new UserService();
			
			User user = new User("test", "test", "test@test.com", "00000000", "test", "test", "0000", "test");
			//When
			userService.AddUser(user);
			//Then
			var actual = Database.Users.First(find => find.ID == user.ID);
			Assert.Equal(user, actual);
		}
	}
}
