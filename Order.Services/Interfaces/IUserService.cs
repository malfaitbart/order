using Order.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Services.Interfaces
{
	public interface IUserService
	{
		List<User> GetAll();
		void AddUser(User user);
	}
}
