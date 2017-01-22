using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
	public class JoinUserProject
	{
		public string UserId { get; set; }
        public string UserEmail { get; set; }
		public ApplicationUser User { get; set; }

		public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
		public Project Project { get; set; }
	}
}
