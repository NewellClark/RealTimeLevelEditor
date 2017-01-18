using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApi.Models
{
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : IdentityUser
	{
		public virtual ICollection<LevelDbEntry> Levels { get; set; }

		public virtual ICollection<JoinUserProject> Projects { get; set; }
	}
}
