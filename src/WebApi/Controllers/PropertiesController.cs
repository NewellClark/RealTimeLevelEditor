using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    public class PropertyDTO
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    [Route("api/properties")]
    public class PropertiesController : Controller
    {
        // GET: api/values
        ApplicationDbContext _db;

        public PropertiesController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{levelId}/{propertyId}")]
        public IEnumerable<PropertyDTO> Get(string levelId, string propertyId)
        {
            return _db.TypeProperties.Where(x => x.LevelId == levelId && x.Name == propertyId)
                .Select(m =>
                new PropertyDTO
                {
                    name = m.Property,
                    value = m.Value
                }).ToList();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
