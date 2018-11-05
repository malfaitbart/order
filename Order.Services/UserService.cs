using Order.Data;
using Order.Domain.Users;
using Order.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Services
{
	public class UserService : IUserService
	{
		public void AddUser(User user)
		{
			Database.Users.Add(user);
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
