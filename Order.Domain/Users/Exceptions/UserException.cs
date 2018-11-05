using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Users.Exceptions
{
	public class UserException : Exception
	{
		public UserException(string message) : base(message)
		{
		}
	}
}
