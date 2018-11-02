namespace Order.API.Controllers.Items
{
	public class ItemDTO
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public int Amount { get; set; }

		public ItemDTO(int iD, string name, string description, double price, int amount)
		{
			ID = iD;
			Name = name;
			Description = description;
			Price = price;
			Amount = amount;
		}
	}
}