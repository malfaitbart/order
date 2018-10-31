using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Domain.Users;
using Order.Services;
using Order.Services.Interfaces;

namespace Order.API.Controllers.Users
{
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

		[HttpPost]
		public ActionResult<User> AddCustomer([FromBody]UserDTO userToRegister)
		{
			try
			{
				userService.AddUser(userMapper.UserDTOToUser(userToRegister));
				return Ok();
			}
			catch (Exception)
			{

				throw;
			}
		}
    }
}