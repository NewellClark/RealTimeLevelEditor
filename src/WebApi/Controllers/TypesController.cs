using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using Newtonsoft.Json;
using WebApi.Models;

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


        }

        // POST api/values
        [HttpPost("{levelId}")]
        public void Post(string levelId, [FromBody]TypeDTO type)
        {
            
            var addType = new TypeDbEntry
            {
                EditorModel = type.inGameModel,
                InGameModel = type.tileModel,
                PropertiesJSON = type.name,
                LevelId = levelId    
            };

            
            _db.TilesTypes.Add(addType);
            _db.SaveChanges();
            


        }

        // PUT api/values/5
        [HttpPut("{levelId}/{typeName}")]
        public void Put(string levelId, string typeName, [FromBody]TypeDTO typeUpdate)
        {
            TypeDbEntry type = _db.TilesTypes.FirstOrDefault(x => x.LevelId == levelId && x.PropertiesJSON == typeName);

            type.PropertiesJSON = typeUpdate.name;
            type.EditorModel = typeUpdate.tileModel;
            type.InGameModel = typeUpdate.inGameModel;

            _db.Update(type);
            _db.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{levelId}/{typeName}")]
        public void Delete(string levelId, string typeName)
        {
            TypeDbEntry type = _db.TilesTypes.FirstOrDefault(x => x.LevelId == levelId && x.PropertiesJSON == typeName);
            _db.Remove(type);
            _db.SaveChanges();
        }
    }
}
