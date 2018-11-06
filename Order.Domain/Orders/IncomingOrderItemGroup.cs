using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Orders
{
	public class IncomingOrderItemGroup
	{
		public int ItemID { get; private set; }
		public int ItemAmount { get; private set; }

		public IncomingOrderItemGroup(int itemID, int itemAmount)
		{
			ItemID = itemID;
			ItemAmount = itemAmount;
		}
	}
}
