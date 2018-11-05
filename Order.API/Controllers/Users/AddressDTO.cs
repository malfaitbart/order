using Order.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Controllers.Users
{
	public class AddressDTO
	{
		public string Street { get;  set; }
		public string Number { get;  set; }
		public string PostalCode { get;  set; }
		public string City { get;  set; }

		public AddressDTO(string street, string number, string postalCode, string city)
		{
			Street = street;
			Number = number;
			PostalCode = postalCode;
			City = city;
		}

		//public AddressDTO(Address address)
		//{
		//	Street = address.Street;
		//	Number = address.Number;
		//	PostalCode = address.PostalCode;
		//	City = address.City;
		//}
	}
}
