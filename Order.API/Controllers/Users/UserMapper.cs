using Order.Domain.Users;

namespace Order.API.Controllers.Users
{
	public class UserMapper
	{
		public UserDTO UserToUserDTO(User user)
		{
			var userdto = new UserDTO
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

		public User UserDTOToUser(UserDTO userDTO)
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
	}
}
