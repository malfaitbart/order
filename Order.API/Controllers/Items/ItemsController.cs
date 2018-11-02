using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Services.Interfaces;

namespace Order.API.Controllers.Items
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
		private readonly IItemService itemService;
		private readonly ItemMapper itemMapper;

		public ItemsController(IItemService itemService, ItemMapper itemMapper)
		{
			this.itemService = itemService;
			this.itemMapper = itemMapper;
		}

		[AllowAnonymous]
		[HttpGet]
		public ActionResult GetAll()
		{
			return Ok(itemMapper.ItemListToIteMDTOList(itemService.GetAll()));
		}

		[AllowAnonymous]
		[HttpGet("{id}", Name = "GetItem")]
		public ActionResult<ItemDTO> GetItemByID(int id)
		{
			var result = itemService.GetByID(id);
			if (result == null)
			{
				return NotFound();
			}
			return Ok(itemMapper.ItemToItemDTO(result));
		}

		[Authorize(Policy = "Admin")]
		[HttpPost]
		public ActionResult<ItemDTO_Add> AddItem(ItemDTO_Add itemDTO)
		{
			itemService.AddItem(itemMapper.ItemDTO_AddToItem(itemDTO));
			return Ok(itemDTO);
		}
	}
}