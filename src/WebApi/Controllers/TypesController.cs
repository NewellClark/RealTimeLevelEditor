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
		public string TileModel { get; set; }
		public string InGameModel { get; set; }
		
		public string Name { get; set; }
	}

	[Route("api/types")]
	public class TypesController : Controller
	{
		public TypesController(ApplicationDbContext db)
		{
			_db = db;
		}
	 
		// GET api/values/5
		[HttpGet("{projectId}")]
		public IEnumerable<TypeDTO> Get(string projectId)
		{
			return _db.TilesTypes.Where(x => x.LevelId == projectId)
				.Select(m =>
				new TypeDTO
				{
					InGameModel = m.InGameModel,
					TileModel = m.EditorModel,
					Name = m.PropertiesJSON
				}).ToList();
		}

		// POST api/values
		[HttpPost("{projectId}")]
		public void Post(string projectId, [FromBody]TypeDTO type)
		{
			var addType = new TypeDbEntry
			{
				EditorModel = type.TileModel,
				InGameModel = type.InGameModel,
				PropertiesJSON = type.Name,
				LevelId = projectId    
			};
			
			_db.TilesTypes.Add(addType);
			_db.SaveChanges();
		}

		// PUT api/values/5
		[HttpPut("{projectId}/{typeName}")]
		public void Put(string projectId, string typeName, [FromBody]TypeDTO typeUpdate)
		{
			TypeDbEntry type = _db.TilesTypes.FirstOrDefault(x => x.LevelId == projectId && x.PropertiesJSON == typeName);

			type.PropertiesJSON = typeUpdate.Name;
			type.EditorModel = typeUpdate.TileModel;
			type.InGameModel = typeUpdate.InGameModel;

			_db.Update(type);
			_db.SaveChanges();

		}

		// DELETE api/values/5
		[HttpDelete("{projectId}/{typeName}")]
		public void Delete(string projectId, string typeName)
		{
			TypeDbEntry type = _db.TilesTypes.FirstOrDefault(x => x.LevelId == projectId && x.PropertiesJSON == typeName);
			_db.Remove(type);
			_db.SaveChanges();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_db?.Dispose();
			}
			base.Dispose(disposing);
		}

		private readonly ApplicationDbContext _db;
	}
}
