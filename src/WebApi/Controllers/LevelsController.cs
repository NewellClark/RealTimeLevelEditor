using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.ViewModels.Levels;
using WebApi.Services;
using RealTimeLevelEditor;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
	[Route(uri)]
	public class LevelsController : Controller
	{
		public LevelsController(
			ILoadedLevelService<string> levelLoader, 
			ApplicationUserService userService)
		{
			_levelLoader = levelLoader;
			_userService = userService;
		}

		/// <summary>
		/// Gets all the levels for the currently-authenticated user.
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpGet]
		public IEnumerable<LevelInfoViewModel> GetAllLevels()
		{
			string userId = _userService.WithUserName(User.Identity.Name)?.Id;
			return _levelLoader.FindLevelsForUser(userId);
		}

		/// <summary>
		/// Creates a new level belonging to the currently-authenticated user and returns some information
		/// about it.
		/// </summary>
		/// <param name="levelViewModel"></param>
		/// <returns></returns>
	//	[Authorize]
		[HttpPost("{projectId}")]
		public IActionResult CreateLevel(Guid projectId, [FromBody]CreateLevelViewModel levelViewModel)
		{
			if (!ModelState.IsValid || levelViewModel == null)
			{
				return BadRequest(ModelState);
			}
			string userId = GetUserId();
			var result = _levelLoader.Create(userId, projectId, levelViewModel.Name);

			return Created(GetLevelUri(result.Info), result.Info);
		}

		/// <summary>
		/// Deletes the level with the specified id.
		/// </summary>
		/// <param name="levelId"></param>
		/// <returns></returns>
		[Authorize]
		[HttpDelete("{levelId}")]
		public IActionResult DeleteLevel(Guid levelId)
		{
			if (!_levelLoader.Exists(levelId))
				return NotFound($"The specified ID ({levelId}) does not exist.");

			if (!CurrentUserOwnsLevel(levelId))
				return Forbid();

			_levelLoader.Delete(levelId);

			return Ok();
		}

		//[Authorize]
		[HttpPost("{levelId}/region")]
		public IActionResult LoadRegion(Guid levelId, [FromBody]LoadLevelRegionViewModel region)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_levelLoader.Exists(levelId))
				return GetLevelNotFoundResult(levelId);

			//if (!CurrentUserOwnsLevel(levelId))
			//	return Forbid();

			var level = _levelLoader.Load(levelId).Level;
			var chunks = level.GetChunksInRegion(region.Region);

			return Ok(chunks);
		}

		//[Authorize]
		[HttpPost("{levelId}/tiles")]
		public IActionResult LoadTiles(Guid levelId, [FromBody]IEnumerable<TileIndex> indecesToLoad)
		{
			if (!_levelLoader.Exists(levelId))
				return GetLevelNotFoundResult(levelId);
			//if (!CurrentUserOwnsLevel(levelId))
			//	return Forbid();

			var level = _levelLoader.Load(levelId).Level;
			var results = level.GetExistingTiles(indecesToLoad);

			return Ok(results);
		}

	//	[Authorize]
		[HttpPut("{levelId}/tiles")]
		public IActionResult AddOrUpdate(Guid levelId, [FromBody]IEnumerable<Tile<string>> tiles)
		{
			if (!_levelLoader.Exists(levelId))
				return NotFound($"There is no level with an id of {levelId}");
			//if (!CurrentUserOwnsLevel(levelId))
			//	return Forbid();

			var level = _levelLoader.Load(levelId);
			level.Level.AddOrUpdate(tiles);

			return Ok(tiles.Select(x => x.Index));
		}

		//[Authorize]
		[HttpDelete("{levelId}/region")]
		public IActionResult DeleteRegion(Guid levelId, [FromBody]LoadLevelRegionViewModel region)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			if (!_levelLoader.Exists(levelId))
				return GetLevelNotFoundResult(levelId);
			if (!CurrentUserOwnsLevel(levelId))
				return Forbid();

			var level = _levelLoader.Load(levelId).Level;
			level.Delete(region.Region);

			return Ok();
		}

		//[Authorize]
		[HttpDelete("{levelId}/tiles")]
		public IActionResult DeleteTiles(Guid levelId, [FromBody]IEnumerable<TileIndex> tileIndeces)
		{
			if (!_levelLoader.Exists(levelId))
				return GetLevelNotFoundResult(levelId);
			if (!CurrentUserOwnsLevel(levelId))
				return Forbid();

			var level = _levelLoader.Load(levelId).Level;
			level.Delete(tileIndeces);

			return Ok();
		}


		private IActionResult GetLevelNotFoundResult(Guid levelId)
		{
			return NotFound($"The requested level (id: {levelId}) does not exist.");
		}

		private string GetLevelUri(LevelInfoViewModel level)
		{
			return $"{uri}/{level.LevelId}";
		}

		private bool CurrentUserOwnsLevel(Guid levelId)
		{
			var level = _levelLoader.GetInfo(levelId);
			return GetUserId() == level?.OwnerId;
		}

		private string GetUserId(string userName)
		{
			return _userService.WithUserName(User.Identity.Name)?.Id;
		}
		private string GetUserId()
		{
			return GetUserId(User.Identity.Name);
		}

		private ILoadedLevelService<string> _levelLoader;
		private ApplicationUserService _userService;
		private const string uri = "api/levels";
	}
}
