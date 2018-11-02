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
			(
				user.FirstName,
				user.LastName,
				user.Email,
				user.PhoneNumber,
				user.Street,
				user.Number,
				user.PostalCode,
				user.City
			);
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

		public UserDTO UserToUserDTO_GetAll(User user)
		{
			var userdto = new UserDTO(
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

		public List<UserDTO> UserListToUserDTO_GetAllList(List<User> users)
		{
			var dtoList = new List<UserDTO>();
			foreach (var user in users)
			{
				dtoList.Add(UserToUserDTO_GetAll(user));
			}
			return dtoList;
		}
	}
}
