using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Controllers.Items
{
	public class ItemDTOForStockResupply
	{
		public enum StockLevels { STOCK_LOW, STOCK_MEDIUM, STOCK_HIGH }

		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public int StockAmount { get; set; }
		public string StockLevel { get; private set; }

		public ItemDTOForStockResupply(int iD, string name, string description, double price, int stockAmount)
		{
			ID = iD;
			Name = name;
			Description = description;
			Price = price;
			StockAmount = stockAmount;
			StockLevel = DetermineStockLevelBasedOnStockAmount();
		}

		private string DetermineStockLevelBasedOnStockAmount()
		{
			if(StockAmount < 5)
			{
				return StockLevels.STOCK_LOW.ToString();
			}
			if(StockAmount < 10)
			{
				return StockLevels.STOCK_MEDIUM.ToString();
			}
			return StockLevels.STOCK_HIGH.ToString();
		}
	}
}
