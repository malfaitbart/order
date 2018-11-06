using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Domain.Items.Exceptions;
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
		private readonly ILogger<ItemsController> logger;

		public ItemsController(IItemService itemService, ItemMapper itemMapper, ILogger<ItemsController> logger)
		{
			this.itemService = itemService;
			this.itemMapper = itemMapper;
			this.logger = logger;
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
		public ActionResult<ItemDTOWithoutID> AddItem(ItemDTOWithoutID itemDTO)
		{
			itemService.AddItem(itemMapper.ItemDTO_AddToItem(itemDTO));
			return Ok(itemDTO);
		}

		[Authorize(Policy = "Admin")]
		[HttpPut("{id}")]
		public ActionResult UpdateItem(int id, [FromBody]ItemDTOWithoutID itemDTOWithoutID)
		{
			try
			{
			itemService.UpdateItem(id, itemDTOWithoutID.Name, itemDTOWithoutID.Description, itemDTOWithoutID.Price, itemDTOWithoutID.Amount);
			return Ok(GetItemByID(id));
			}
			catch (ItemException ex)
			{
				var errorid = Guid.NewGuid();
				logger.LogError(errorid + " " + ex.Message);
				return BadRequest(errorid + " " + ex.Message);
			}
		}
	}
}