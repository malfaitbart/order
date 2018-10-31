using Order.Data;
using Order.Domain.Users;
using System.Collections.Generic;

namespace Order.API.Controllers.Users
{
	public class UserMapper
	{
		public UserDTO_Register UserToUserDTO(User user)
		{
			var userdto = new UserDTO_Register
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				Street = user.Street,
				Number = user.Number,
				PostalCode = user.PostalCode,
				City = user.City
			};
			return userdto;
		}

		public User UserDTOToUser(UserDTO_Register userDTO)
		{
			var user = new User(
				userDTO.FirstName,
				userDTO.LastName,
				userDTO.Email,
				userDTO.PhoneNumber,
				userDTO.Street,
				userDTO.Number,
				userDTO.PostalCode,
				userDTO.City
			);
			return user;
		}

		public UserDTO_GetAll UserToUserDTO_GetAll(User user)
		{
			var userdto = new UserDTO_GetAll(
				user.ID,
				user.FirstName,
				user.LastName,
				user.Email,
				user.PhoneNumber,
				user.Street,
				user.Number,
				user.PostalCode,
				user.City,
				Database.UserRoles[user.RoleID],
				Database.UserStatus[user.Status]);
			return userdto;
		}

		public List<UserDTO_GetAll> UserListToUserDTO_GetAllList(List<User> users)
		{
			var dtoList = new List<UserDTO_GetAll>();
			foreach (var user in users)
			{
				dtoList.Add(UserToUserDTO_GetAll(user));
			}
			return dtoList;
		}
	}
}
