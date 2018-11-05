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
		public Address Address{ get; private set; }
		public int RoleID { get; private set; }
		public int Status { get; private set; }
		private static int CreateID;

		public User(string firstName, string lastName, string email, string phoneNumber, Address address)
		{
			ID = CreateID;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
			Address = address;
			RoleID = 1;
			Status = 1;

			CreateID++;
		}

		public User(string firstName, string lastName, string email, string phoneNumber, Address address, int roleID)
		{
			ID = CreateID;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			PhoneNumber = phoneNumber;
			Address = address;
			RoleID = roleID;
			Status = 1;

			CreateID++;
		}

		public void SetAdmin()
		{
			RoleID = 2;
		}
	}
}
