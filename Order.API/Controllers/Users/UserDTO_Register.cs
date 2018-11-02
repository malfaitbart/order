namespace Order.API.Controllers.Users
{
	public class UserDTO_Register
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Street { get; set; }
		public string Number { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }

		public UserDTO_Register(string firstName, string lastName, string email, string phoneNumber, string street, string number, string postalCode, string city)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
			Street = street;
			Number = number;
			PostalCode = postalCode;
			City = city;
		}
	}
}
