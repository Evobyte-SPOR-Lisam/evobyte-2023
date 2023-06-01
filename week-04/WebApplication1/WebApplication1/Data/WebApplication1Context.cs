using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
	public class WebApplication1Context : DbContext
	{
		public WebApplication1Context(DbContextOptions<WebApplication1Context> options)
			: base(options)
		{
		}

		public DbSet<Models.Person> Person { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder
				.Entity<Models.Person>()
				.HasKey(nameof(Models.Person.ID2), nameof(Models.Person.DateOfBirth));
		}
	}
}
