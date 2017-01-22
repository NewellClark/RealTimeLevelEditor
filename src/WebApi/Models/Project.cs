using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
	public class Project
	{
		[Key]
		public Guid Id { get; set; }

		public string OwnerId { get; set; }

        public string Name { get; set; }

		public virtual ICollection<JoinUserProject> Members { get; set; }

		public virtual ICollection<LevelDbEntry> Levels { get; set; }
	}
}
