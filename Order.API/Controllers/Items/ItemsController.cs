using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Domain.Items.Exceptions;
using Order.Services.Interfaces;
using System;
using System.Collections.Generic;

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
			return Ok(itemMapper.ToDTOList(itemService.GetAll()));
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
			return Ok(itemMapper.ToDTO(result));
		}

		[Authorize(Policy = "Admin")]
		[HttpPost]
		public ActionResult<ItemDTOWithoutID> AddItem(ItemDTOWithoutID itemDTO)
		{
			itemService.AddItem(itemMapper.ToItem(itemDTO));
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

		[Authorize(Roles = "Admin")]
		[HttpGet("[action]")]
		public ActionResult<List<ItemDTOForStockResupply>> GetItemsOrderedByResupplyNeeds([FromQuery]string indicator)
		{
			if (indicator == null)
			{
				return itemMapper.toDTOFotStockResupplyList(itemService.GetAllSortedByStock());
			} else
			{
				try
				{
				return itemMapper.toDTOFotStockResupplyList(itemService.GetAllSortedByStock(indicator));
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
}