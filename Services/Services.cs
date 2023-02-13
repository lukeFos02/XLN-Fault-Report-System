using XLN_Fault_Report_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace XLN_Fault_Report_System.Services
{
	public class Services : IServices
	{
		private readonly LoginDbcontext _context;
		public Services(LoginDbcontext context)
		{
			_context = context;
		}
		public bool AuthenticateUser(string username, string passcode)
		{
            var succeeded = _context.Users.FirstOrDefault(authUser => authUser.UserName == username && authUser.Password == passcode);
			if (succeeded == null)
			{
				return false;
			}
			else
			{
				return true;
			}
        }
		public User GetUser(string username, string passcode)
		{
            var User = _context.Users.FirstOrDefault(authUser => authUser.UserName == username && authUser.Password == passcode);
			return User;
		}
		public List<Asset> GetUsersAssets(string username, string passcode)
		{
            var User = _context.Users.FirstOrDefault(authUser => authUser.UserName == username && authUser.Password == passcode);
			var AssetList = _context.Assets.ToList();
			var results = AssetList.Where(a => a.UserId == User.UserId);
            List<Asset> assets = results.ToList();
			return assets;
        }
	}
	public interface IServices
	{
		User GetUser(string username, string passcode);
		bool AuthenticateUser(string username, string passcode);
		List<Asset> GetUsersAssets(string username, string passcode);
	}
}
