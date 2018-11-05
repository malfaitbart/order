namespace Order.API.Controllers.Orders
{
	public class OrderDTO_Create
	{
		public int ItemID{ get; set; }
		public int ItemAmount { get; set; }

		public OrderDTO_Create(int itemID, int itemAmount)
		{
			this.ItemID = itemID;
			this.ItemAmount = itemAmount;
		}
	}
}