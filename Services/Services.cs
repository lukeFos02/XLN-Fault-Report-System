using XLN_Fault_Report_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace XLN_Fault_Report_System.Services
{
	public class Services : IServices
	{
		private readonly Dbcontext _context;
		public Services(Dbcontext context)
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
		public Asset GetAsset(int id)
		{
			var asset = _context.Assets.FirstOrDefault(a => a.AssetId == id);
			return asset as Asset;
		}
	}
	public interface IServices
	{
		User GetUser(string username, string passcode);
		bool AuthenticateUser(string username, string passcode);
		List<Asset> GetUsersAssets(string username, string passcode);
		Asset GetAsset(int id);	
	}
}
