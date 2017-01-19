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
    public class UserIdDTO {
        public string UserId;
    }

    [Route("api/projects")]
    public class TeamController : Controller
    {
        ApplicationDbContext _db;

        public TeamController(ApplicationDbContext db)
        {
            _db = db;
        }


        // GET: api/values
       // [HttpGet]
       // public IEnumerable<string> Get()
       // {
            //return _db.UserProjects.Where(x=> x.ProjectId == projectId);
       // }

        // GET api/values/5
        [HttpGet("{projectId}")]
        public IEnumerable<UserIdDTO> Get(Guid projectId)
        {
            
            return _db.UserProjects.Where(x => x.ProjectId == projectId)
                .Select(m =>
                new UserIdDTO
                {
                    UserId = m.UserId
                }).ToList();
        }

        // Create team
        [HttpPost("{ownerId}/{projectName}")]
        public void Post(string ownerId, string projectName, [FromForm] string name)
        {
            //todo: get actual user Id 
           // string opl = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();

            Guid guid;
            guid = Guid.NewGuid();

            var newProject = new Project
            {
                Id = guid,
                OwnerId = ownerId,
                Levels = null,
                Members = null
            };


            _db.Projects.Add(newProject);
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
