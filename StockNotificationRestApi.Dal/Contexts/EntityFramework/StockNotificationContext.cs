using Microsoft.EntityFrameworkCore;
using StockNotificationRestApi.Entities.Concretes;

namespace StockNotificationRestApi.Dal.Contexts.EntityFramework
{
	public class StockNotificationContext : DbContext
	{
		#region DEFINES
		public DbSet<StockNotification> StockNotifications { get; set; }
		#endregion

		#region CONSTRUCTOR
		public StockNotificationContext(DbContextOptions<StockNotificationContext> options) : base(options) { }
		#endregion

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<StockNotification>(b =>
			{
				b.HasKey(x => x.Id);

				b.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
				b.Property(x => x.ProductId).HasMaxLength(256).IsRequired();
				b.Property(x => x.UserId).HasMaxLength(256).IsRequired();
				b.Property(x => x.UserEmail).HasMaxLength(256).IsRequired();
				b.Property(x => x.CreatedByUser).HasMaxLength(256).IsRequired();
				b.Property(x => x.ModifiedByUser).HasMaxLength(256).IsRequired();

				b.HasIndex(x => new { x.ProductId, x.UserId, x.NotificationType }).IsUnique();
			});
		}

	}
}
