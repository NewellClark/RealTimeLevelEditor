using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Services
{
	public class ApplicationUserService
	{
		public ApplicationUserService(ApplicationDbContext db)
		{
			_db = db;
		}

		public ApplicationUser WithUserName(string userName)
		{
			return _db.Users
				.Where(x => x.UserName == userName)
				.SingleOrDefault();
		}

		private ApplicationDbContext _db;
	}
}
