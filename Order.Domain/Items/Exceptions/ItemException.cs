using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Items.Exceptions
{
	public class ItemException : Exception
	{
		public ItemException(string message) : base(message)
		{
		}
	}
}
