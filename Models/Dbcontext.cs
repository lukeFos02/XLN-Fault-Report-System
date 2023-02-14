using Microsoft.EntityFrameworkCore;
namespace XLN_Fault_Report_System.Models
{
	public class Dbcontext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Asset> Assets { get; set; }
		public Dbcontext(DbContextOptions<Dbcontext> options) : base(options)
		{

		}
        public Dbcontext()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<User>(entity => {
				entity.HasKey(k => k.UserId);
			});
            builder.Entity<Asset>(entity => {
                entity.HasKey(k => k.AssetId);
            });
        }
	}
}
