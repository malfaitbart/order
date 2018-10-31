using Order.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Services.Interfaces
{
	public interface IUserService
	{
		void AddUser(User user);
	}
}
