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
        [HttpPost("{levelId}/{propertyId}")]
        public void Post(string levelId, string propertyId, [FromBody] IEnumerable<PropertyDTO> properties)
        {
            var v = _db.TypeProperties.Where(x => x.LevelId == levelId && x.Name == propertyId);
            _db.Remove(v);
            _db.SaveChanges();

            _db.Add(properties);
            _db.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{levelId}/{propertyId}/{name}")]
        public void Put(string levelId, string propertyId, string name, [FromBody]string value)
        {
            TypeProperty typeProperty = _db.TypeProperties.FirstOrDefault(x => x.LevelId == levelId 
                                                                          && x.Name == propertyId
                                                                          && x.Property == name);
            typeProperty.Value = value;
            _db.SaveChanges();
                        
        }

        // DELETE api/values/5
        [HttpDelete("{levelId}/{propertyId}/{name}")]
        public void Delete(string levelId, string propertyId, string name)
        {
            TypeProperty typeProperty = _db.TypeProperties.FirstOrDefault(x => x.LevelId == levelId
                                                                          && x.Name == propertyId
                                                                          && x.Property == name);
            _db.Remove(typeProperty);
            _db.SaveChanges();
        }
    }
}
