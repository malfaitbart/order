namespace Order.API.Controllers.Items
{
	public class ItemDTOWithoutID
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public int Amount { get; set; }

		public ItemDTOWithoutID(string name, string description, double price, int amount)
		{
			Name = name;
			Description = description;
			Price = price;
			Amount = amount;
		}
	}
}