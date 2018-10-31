using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Users
{
	public class User
	{
		public int ID { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string Email { get; private set; }
		public string PhoneNumber { get; private set; }
		public string Street { get; private set; }
		public string Number { get; private set; }
		public string PostalCode { get; private set; }
		public string City { get; private set; }
		public int RoleID { get; private set; }
		public int Status { get; private set; }

		private static int CreateID;

		public User(string firstName, string lastName, string email, string phoneNumber, string street, string number, string postalCode, string city)
		{
			ID = CreateID;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
			Street = street;
			Number = number;
			PostalCode = postalCode;
			City = city;
			RoleID = 1;
			Status = 1;

			CreateID++;
		}
	}
}
