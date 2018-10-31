using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Controllers.Users
{
	public class UserDTO_GetAll
	{
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Street { get; set; }
		public string Number { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
		public string Role { get; set; }
		public string Status { get; set; }

		public UserDTO_GetAll(int iD, string firstName, string lastName, string email, string phoneNumber, string street, string number, string postalCode, string city, string role, string status)
		{
			ID = iD;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
			Street = street;
			Number = number;
			PostalCode = postalCode;
			City = city;
			Role = role;
			Status = status;
		}
	}
}
