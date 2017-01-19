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
    [Route("api/teams")]
    public class TeamController : Controller
    {
        ApplicationDbContext _db;

        public TeamController(ApplicationDbContext db)
        {
            _db = db;
        }


        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // Create team
        [HttpPost("{ownerId}/{projectName}")]
        public void Post(string ownerId, string projectName)
        {
            //todo: get actual user Id 
            string opl = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();

            var newProject = new Project
            {
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
