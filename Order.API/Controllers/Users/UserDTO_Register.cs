namespace Order.API.Controllers.Users
{
	public class UserDTO_Register
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public AddressDTO AddressDTO { get; set; }

		public UserDTO_Register(string firstName, string lastName, string email, string phoneNumber,AddressDTO addressDTO)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
			AddressDTO = addressDTO;
		}
	}
}
