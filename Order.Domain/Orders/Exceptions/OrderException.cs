using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Orders.Exceptions
{
	public class OrderException : Exception
	{
		public OrderException(string message) : base(message)
		{
		}
	}
}
