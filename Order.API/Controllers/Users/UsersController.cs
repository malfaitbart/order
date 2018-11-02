using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Domain.Users;
using Order.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Order.API.Controllers.Users
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService userService;
		private readonly UserMapper userMapper;

		public UsersController(IUserService userService, UserMapper userMapper)
		{
			this.userService = userService;
			this.userMapper = userMapper;
		}

		[Authorize(Policy = "Admin")]
		[HttpGet]
		public ActionResult<List<UserDTO>> GetAll()
		{
			return userMapper.UserListToUserDTO_GetAllList(userService.GetAll());
		}

		[Authorize(Policy = "Admin")]
		[HttpGet("{id}", Name = "GetUser")]
		public ActionResult<UserDTO> GetUserByID(int id)
		{
			var result = userService.GetUserByID(id);
			if (result == null)
			{
				return NotFound();
			}
			return Ok(userMapper.UserToUserDTO(result));
		}

		[AllowAnonymous]
		[HttpPost]
		public ActionResult<UserDTO> AddCustomer([FromBody]UserDTO_Register userToRegister)
		{
			try
			{
				var user = userMapper.UserDTOToUser(userToRegister);
				userService.AddUser(user);
				return CreatedAtRoute("GetUser", new { id = user.ID }, userMapper.UserToUserDTO(user));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}