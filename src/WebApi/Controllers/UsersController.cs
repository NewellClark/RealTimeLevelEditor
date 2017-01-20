using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{


    [Route("api/members")]
    public class UsersController : Controller
    {
        public class ProjectIdDTO
        {
            public Guid ProjectId;
        }

        ApplicationDbContext _db;

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        } 

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{userId}")]
        public IEnumerable<ProjectIdDTO> Get(string userId)
        {

            return _db.UserProjects.Where(x => x.UserId == userId)
                .Select(m =>
                new ProjectIdDTO
                {
                    ProjectId = m.ProjectId
                }).ToList();
        }

        // POST api/values
        [HttpPost("{projectId}/{userId}")]
        public void Post(Guid projectId, string userId)
        {
            //todo: get actual user Id 
            // string opl = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();

            //Guid guid;
            //guid = Guid.NewGuid();
            var theProject = _db.Projects.FirstOrDefault(x => x.Id == projectId);
            var theUser = _db.Users.FirstOrDefault(x => x.Id == userId);

            var newUserProject = new JoinUserProject
            {
                ProjectId = projectId,
                Project = theProject,
                UserId = userId,
                User = theUser    
            };

            
            _db.UserProjects.Add(newUserProject);
            _db.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
