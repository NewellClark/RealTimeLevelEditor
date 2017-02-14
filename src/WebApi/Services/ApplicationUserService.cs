using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Services
{
	public sealed class ApplicationUserService : IDisposable
	{
		public ApplicationUserService(ApplicationDbContext db)
		{
			_db = db;
		}

		public ApplicationUser WithUserName(string userName)
		{
			ThrowIfDisposed();

			return _db.Users
				.Where(x => x.UserName == userName)
				.SingleOrDefault();
		}

		private ApplicationDbContext _db;
		private bool _isDisposed;

		public void Dispose()
		{
			if (_isDisposed)
				return;

			_db?.Dispose();
			_isDisposed = true;
		}

		private void ThrowIfDisposed()
		{
			if (_isDisposed)
				throw new ObjectDisposedException(GetType().Name);
		}
	}
}
