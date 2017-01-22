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
    public class UserIdDTO {
        public string UserId;
        public string UserEmail;
    }
    public class ProjectIdDTO
    {
        public Guid ProjectId;
        public string ProjectName;
    }

    public class LevelDTO
    {
        public Guid LevelId;
        public string Name;
    }

    [Route("api/projects")]
    public class ProjectController : Controller
    {
        ApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;
        //public UserController(UserManager<ApplicationUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        public ProjectController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;

        }

        //get all the projects of which logged in user is admin
        [HttpGet]
        public ICollection<ProjectIdDTO> Get()
        {
            var userId = _userManager.GetUserId(User);
            return _db.Projects.Where(x => x.OwnerId == userId)
                            .Select(m =>
                            new ProjectIdDTO
                            {
                                ProjectId = m.Id,
                                ProjectName = m.Name
                            }).ToList();
        }

       
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

        
        //get all the levels in a project
        [HttpGet("{projectId}/levels")]
        public IEnumerable<LevelDTO> Get(Guid projectId, string dummy)
        {

            var theProject = _db.Levels.FirstOrDefault(x => x.Id == projectId);

          return _db.Levels.Where(x => x.ProjectId == projectId)
            .Select(m =>
            new LevelDTO
            {
                LevelId = m.Id,
                Name = m.Name
            }).ToList();
        }
        
        // Create team
        [HttpPost("{newProjectName}")]
        public void Post(string newProjectName, [FromForm] string name)
        {

            Guid guid;
            guid = Guid.NewGuid();

            //get ID of logged in user
            var userId = _userManager.GetUserId(User);

            var newProject = new Project
            {
                Id = guid,
                OwnerId = userId,
                Name = newProjectName,
                Levels = null,
                Members = null
            };


            _db.Projects.Add(newProject);
            _db.SaveChanges();
        }

        //add user to project
        [HttpPost("{projectId}/{userEmail}")]
        public void Post(Guid projectId, string userEmail, [FromForm] string name) {

            //todo make sure user is the project owner

            //get the user id from the user email
        var theUser = _db.Users.FirstOrDefault(x => x.Email == userEmail);
        var theProject = _db.Projects.FirstOrDefault(x => x.Id == projectId);

        
        var newUserProject = new JoinUserProject
        {
            ProjectId = theProject.Id,
            ProjectName = theProject.Name,
            UserId = theUser.Id,
            UserEmail = theUser.Email,

            Project = theProject,
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
        [HttpDelete("{projectId}")]
        public void Delete(Guid projectId)
        {
            var theProject = _db.Projects.FirstOrDefault(x => x.Id == projectId);
            _db.Remove(theProject);
            _db.SaveChanges();
        }
    }
}
