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
            var succeeded = _context.Users.FirstOrDefault(authUser => authUser.UserName == username && authUser.passcode == passcode);
			if (succeeded == null)
			{
				return false;
			}
			else
			{
				return true;
			}
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
