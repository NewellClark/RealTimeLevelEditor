using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{


    public class TypeDTO
    {
        public string tileModel { get; set; }
        public string inGameModel { get; set; }
        
        public string name { get; set; }
    }

    [Route("api/types")]
    public class TypesController : Controller
    {

        ApplicationDbContext _db;

        public TypesController(ApplicationDbContext db)
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
        [HttpGet("{levelId}")]
        public IEnumerable<TypeDTO> Get(string levelId)
        {

            return _db.TilesTypes.Where(x => x.LevelId == levelId)
                .Select(m =>
                new TypeDTO
                {
                    inGameModel = m.InGameModel,
                    tileModel = m.EditorModel,
                    name = m.PropertiesJSON
                }).ToList();
            //            return _db.TilesTypes.Select(m => new { string = m. m.LevelId == levelId).ToList();


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
