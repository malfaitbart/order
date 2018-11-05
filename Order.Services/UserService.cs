using Order.Data;
using Order.Domain.Users;
using Order.Domain.Users.Exceptions;
using Order.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Order.Services
{
	public class UserService : IUserService
	{
		public void AddUser(User user)
		{
			if (!IsEmailValid(user.Email))
			{
				throw new FormatException("E-mailaddress is not in valid format(example: bla@bla.com)");
			}
			if (doesEmailAlreadyExist(user.Email))
			{
				throw new UserException("E-MailAddress already exists!");
			}
			Database.Users.Add(user);
		}

		private bool doesEmailAlreadyExist(string email)
		{
			return Database.Users.Any(user => user.Email == email);
		}

		private bool IsEmailValid(string email)
		{
			try
			{
				MailAddress m = new MailAddress(email);
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}

		public List<User> GetAll()
		{
			return Database.Users;
		}

		public async Task<User> Authenticate(string username, string password)
		{
			var user = await Task.Run(() => Database.Users.SingleOrDefault(userToLogin => userToLogin.Email == username));

			if (user == null)
			{
				return null;
			}
			return user;
		}

		public User GetUserByID(int id)
		{
			return Database.Users.FirstOrDefault(user => user.ID == id);
		}

		public int GetUserID(string email)
		{
			var customer = Database.Users.FirstOrDefault(user => user.Email == email);
			return customer.ID;
		}
	}
}
