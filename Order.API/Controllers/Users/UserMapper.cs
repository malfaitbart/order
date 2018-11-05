using Order.Data;
using Order.Domain.Users;
using System.Collections.Generic;

namespace Order.API.Controllers.Users
{
	public class UserMapper
	{
		public User UserDTOToUser(UserDTO_Register userDTO)
		{
			var user = new User(
				userDTO.FirstName,
				userDTO.LastName,
				userDTO.Email,
				userDTO.PhoneNumber,
				new Address(userDTO.AddressDTO.Street, userDTO.AddressDTO.Number, userDTO.AddressDTO.PostalCode, userDTO.AddressDTO.City)
			);
			return user;
		}

		public UserDTO UserToUserDTO(User user)
		{
			var userdto = new UserDTO(
				user.ID,
				user.FirstName,
				user.LastName,
				user.Email,
				user.PhoneNumber,
				new AddressDTO(user.Address.Street, user.Address.Number, user.Address.PostalCode, user.Address.City),
				Database.UserRoles[user.RoleID],
				Database.UserStatus[user.Status]);
			return userdto;
		}

		public List<UserDTO> UserListToUserDTOList(List<User> users)
		{
			var dtoList = new List<UserDTO>();
			foreach (var user in users)
			{
				dtoList.Add(UserToUserDTO(user));
			}
			return dtoList;
		}
	}
}
