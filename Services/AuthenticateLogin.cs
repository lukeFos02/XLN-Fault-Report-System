using XLN_Fault_Report_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace XLN_Fault_Report_System.Services
{
	public class AuthenticateLogin : ILogin
	{
		private readonly LoginDbcontext _context;
		public AuthenticateLogin(LoginDbcontext context)
		{
			_context = context;
		}
		public bool AuthenticateUser(string username, string passcode)
		{
			List<User> users = _context.Users.ToList();
			foreach(User u in users)
			{
				if (username == u.UserName && passcode == u.passcode)
				{
					return true;	
				}
			}
			return false;
		}
		public async Task<IEnumerable<User>> getuser()
		{
			return await _context.Users.ToListAsync();
		}
	}
	public interface ILogin
	{
		Task<IEnumerable<User>> getuser();
		bool AuthenticateUser(string username, string passcode);
	}
}
