using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
     	{

         }

        public DbSet<LevelDbEntry> Levels { get; set; }

		public DbSet<ChunkDbEntry> Chunks { get; set; }

        public DbSet<TypeDbEntry> TilesTypes { get; set; }

        public DbSet<TypeProperty> TypeProperties { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);

			var chunkConfig = builder.Entity<ChunkDbEntry>();
			chunkConfig.HasKey(x => new { x.LevelId, x.X, x.Y });

			var levelConfig = builder.Entity<LevelDbEntry>();
			levelConfig.HasOne(x => x.Owner)
				.WithMany(x => x.Levels)
				.HasForeignKey(x => x.OwnerId);
		}
	}
}
