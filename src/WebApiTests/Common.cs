using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApiTests
{
	public static class Common
	{
		public static ApplicationDbContext GetDbContext()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>();
			string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-WebApi-85a4f310-d208-11e6-9972-95f22171a2aa;Trusted_Connection=True;MultipleActiveResultSets=true";
			options.UseSqlServer(connectionString);
			var db = new ApplicationDbContext(options.Options);

			return db;
		}
	}
}
