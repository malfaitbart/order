using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Order.Domain.Items;

namespace Order.API.Controllers.Items
{
	public class ItemMapper
	{
		internal Item ToItem(ItemDTOWithoutID itemDTOWithoutID)
		{
			Item item = new Item(
				itemDTOWithoutID.Name,
				itemDTOWithoutID.Description,
				itemDTOWithoutID.Price,
				itemDTOWithoutID.Amount,
				1
				);
			return item;
		}

		internal ItemDTO ToDTO(Item item)
		{
			ItemDTO itemDTO = new ItemDTO(
				item.ID,
				item.Name,
				item.Description,
				item.Price,
				item.StockAmount
				);
			return itemDTO;
		}

		internal List<ItemDTO> ToDTOList(List<Item> list)
		{
			List<ItemDTO> itemDTO_GetAlls = new List<ItemDTO>();
			foreach (var item in list)
			{
				itemDTO_GetAlls.Add(ToDTO(item));
			}
			return itemDTO_GetAlls;
		}

		internal List<ItemDTOForStockResupply> toDTOFotStockResupplyList(List<Item> items)
		{
			List<ItemDTOForStockResupply> itemDTOForStockResupplies = new List<ItemDTOForStockResupply>();
			foreach (var item in items)
			{
				itemDTOForStockResupplies.Add(new ItemDTOForStockResupply(item.ID, item.Name, item.Description, item.Price, item.StockAmount));
			}
			return itemDTOForStockResupplies;
		}


	}
}
