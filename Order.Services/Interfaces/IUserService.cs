using Order.Domain.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Services.Interfaces
{
	public interface IUserService
	{
		List<User> GetAll();
		void AddUser(User user);
		Task<User> Authenticate(string username, string password);
		User GetUserByID(int id);
	}
}
