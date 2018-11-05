using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Orders.Exceptions
{
	public class StockException : Exception
	{
		public StockException(string message) : base(message)
		{
		}
	}
}
