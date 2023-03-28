using XLN_Fault_Report_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
		public List<Fault> GetUsersFaults(string username, string passcode)
		{
            var User = _context.Users.FirstOrDefault(authUser => authUser.UserName == username && authUser.Password == passcode);
			var FaultList = _context.Faults.ToList();
			var results = FaultList.Where(f => f.UserId == User.UserId);
			List<Fault> faults = results.ToList();
			return faults;	
        }
		public Asset GetAsset(int id)
		{
			var asset = _context.Assets.FirstOrDefault(a => a.AssetId == id);
			return asset as Asset;
		}
		public void SaveFault(Fault fault)
		{
			_context.Faults.Add(fault);	
			_context.SaveChanges();
		}
		public Fault GetNewFault(int assetid)
		{
			var fault = _context.Faults.FirstOrDefault(a => a.Status == "Fault report pending" && a.AssetId == assetid);
			return fault;
		}
		public void UpdateFaultStatus(List<Fault> faults)
		{
			DateTime now = DateTime.Now;

			foreach (Fault f in faults)
			{
				var fault = _context.Faults.Where(a => a.FaultId == f.FaultId).FirstOrDefault();	
				DateTime faultSubmitDate = DateTime.ParseExact(f.Time, "dd/MM/yyyy HH,mm,ss", CultureInfo.InvariantCulture);
				TimeSpan diff = now - faultSubmitDate;	
				if (diff.Minutes > 15 && fault.Status == "Fault report pending")
				{
					fault.Status = "Fault in progress";
				}
			}
			_context.SaveChanges();
		}	
		public Fault GetFault(int id)
		{
			var fault = _context.Faults.FirstOrDefault(a => a.FaultId == id);	
			return fault as Fault;
		}
		public void CancelFault(Fault chosenfault)
		{
			var fault = _context.Faults.FirstOrDefault(a => a.FaultId == chosenfault.FaultId);
			fault.Status = "Customer cancelled Fault";
			_context.SaveChanges();	

        }
	}
	public interface IServices
	{
		User GetUser(string username, string passcode);
		bool AuthenticateUser(string username, string passcode);
		List<Asset> GetUsersAssets(string username, string passcode);
		List<Fault> GetUsersFaults(string username, string passcode);
		Asset GetAsset(int id);	
		void SaveFault(Fault fault);
		Fault GetNewFault(int assetid);
		void UpdateFaultStatus(List<Fault> faults);
		Fault GetFault(int id);
		void CancelFault(Fault chosenfault);
	}
}
