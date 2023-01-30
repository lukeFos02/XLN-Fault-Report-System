using Microsoft.EntityFrameworkCore;
namespace XLN_Fault_Report_System.Models
{
	public class LoginDbcontext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public LoginDbcontext(DbContextOptions<LoginDbcontext> options) : base(options)
		{

		}
        public LoginDbcontext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<User>(entity => {
				entity.HasKey(k => k.id);
			});
		}
	}
}
