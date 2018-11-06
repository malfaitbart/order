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
		internal ItemDTO ItemToItemDTO(Item item)
		{
			ItemDTO itemDTO = new ItemDTO(
				item.ID,
				item.Name,
				item.Description,
				item.Price,
				item.Amount
				);
			return itemDTO;
		}

		internal List<ItemDTO> ItemListToIteMDTOList(List<Item> list)
		{
			List<ItemDTO> itemDTO_GetAlls = new List<ItemDTO>();
			foreach (var item in list)
			{
				itemDTO_GetAlls.Add(ItemToItemDTO(item));
			}
			return itemDTO_GetAlls;
		}

		internal Item ItemDTO_AddToItem(ItemDTOWithoutID itemDTO)
		{
			Item item = new Item(
				itemDTO.Name,
				itemDTO.Description,
				itemDTO.Price,
				itemDTO.Amount,
				1
				);
			return item;
		}


	}
}
