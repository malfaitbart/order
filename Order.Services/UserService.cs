using Order.Data;
using Order.Domain.Users;
using Order.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Services
{
	public class UserService : IUserService
	{
		public void AddUser(User user)
		{
			Database.Users.Add(user);
		}
	}
}
