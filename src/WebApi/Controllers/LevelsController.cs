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

		[Authorize]
		[HttpGet]
		public IEnumerable<LevelInfoViewModel> FindLevelsForUser()
		{
			string userId = _userService.WithUserName(User.Identity.Name)?.Id;
			return _levelLoader.FindLevelsForUser(userId);
		}

		[Authorize]
		[HttpPost]
		public IActionResult CreateLevel([FromBody]CreateLevelViewModel levelViewModel)
		{
			if (!ModelState.IsValid || levelViewModel == null)
			{
				return BadRequest(ModelState);
			}
			string userId = GetUserId();
			var result = _levelLoader.Create(userId, levelViewModel.Name);

			return Created(GetLevelUri(result.Info), result.Info);
		}

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

		[Authorize]
		[HttpGet]
		public IActionResult LoadLevelRegion([FromBody]LoadLevelRegionViewModel levelRegion)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_levelLoader.Exists(levelRegion.LevelId))
				return NotFound($"The requested level (id: {levelRegion.LevelId}) does not exist.");

			if (!CurrentUserOwnsLevel(levelRegion.LevelId))
				return Forbid();

			var level = _levelLoader.Load(levelRegion.LevelId).Level;
			var chunks = level.GetChunksInRegion(levelRegion.Region);

			return Ok(chunks);
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
