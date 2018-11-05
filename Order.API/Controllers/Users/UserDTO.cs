using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Controllers.Users
{
	public class UserDTO
	{
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public AddressDTO AddressDTO { get; set; }
		public string Role { get; set; }
		public string Status { get; set; }

		public UserDTO(int iD, string firstName, string lastName, string email, string phoneNumber, AddressDTO addressDTO, string role, string status)
		{
			ID = iD;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
			AddressDTO = addressDTO;
			Role = role;
			Status = status;
		}
	}
}
