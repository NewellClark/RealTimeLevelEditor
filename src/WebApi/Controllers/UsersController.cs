using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using Microsoft.AspNetCore.Identity;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{

    //public class UserIdDTO
    //{
    //    public string UserId;
    //    public string UserEmail;
    //}
    //public class ProjectIdDTO
    //{
    //    public Guid ProjectId;
    //    public string ProjectName;
    //}


    [Route("api/members")]
    public class UsersController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;


        //public class ProjectIdDTO
        //{
        //    public Guid ProjectId;
        //}

        ApplicationDbContext _db;

        public UsersController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        } 

        //API dealing with the non-owner users of a project
        [HttpGet]
        //Will return non-owned projects of logged in user
        public IEnumerable<ProjectIdDTO> Get()
        {
            var userId = _userManager.GetUserId(User);

            return _db.UserProjects.Where(x => x.UserId == userId)
                .Select(m =>
                new ProjectIdDTO
                {
                    ProjectId = m.ProjectId,
                    ProjectName = m.ProjectName
                }).ToList();
        }

        //get all the users on a given project referenced by projectId (not project name)
        [HttpGet("{projectId}/getusers")]
        public IEnumerable<UserIdDTO> Get(Guid projectId)
        {

            return _db.UserProjects.Where(x => x.ProjectId == projectId)
                .Select(m =>
                new UserIdDTO
                {
                    UserId = m.UserId,
                    UserEmail = m.UserEmail
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
