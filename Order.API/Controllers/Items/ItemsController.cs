using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Services.Interfaces;
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
		private readonly ILoggerService logger;

		public ItemsController(IItemService itemService, ItemMapper itemMapper, ILoggerService logger)
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
			itemService.UpdateItem(id, itemDTOWithoutID.Name, itemDTOWithoutID.Description, itemDTOWithoutID.Price, itemDTOWithoutID.Amount);
			return Ok(GetItemByID(id));
		}

		[Authorize(Roles = "Admin")]
		[HttpGet("[action]")]
		public ActionResult<List<ItemDTOForStockResupply>> GetItemsOrderedByResupplyNeeds([FromQuery]string indicator)
		{
			if (indicator == null)
			{
				return itemMapper.toDTOFotStockResupplyList(itemService.GetAllSortedByStock());
			}
			else
			{
				return itemMapper.toDTOFotStockResupplyList(itemService.GetAllSortedByStock(indicator));
			}
		}
	}
}