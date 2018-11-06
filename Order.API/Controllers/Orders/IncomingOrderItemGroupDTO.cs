namespace Order.API.Controllers.Orders
{
	public class IncomingOrderItemGroupDTO
	{
		public int ItemID{ get; set; }
		public int ItemAmount { get; set; }

		public IncomingOrderItemGroupDTO(int itemID, int itemAmount)
		{
			this.ItemID = itemID;
			this.ItemAmount = itemAmount;
		}
	}
}