using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Domain.Users
{
	public class Address
	{
		public string Street { get; private set; }
		public string Number { get; private set; }
		public string PostalCode { get; private set; }
		public string City { get; private set; }

		public Address(string street, string number, string postalCode, string city)
		{
			Street = street;
			Number = number;
			PostalCode = postalCode;
			City = city;
		}
	}
}
