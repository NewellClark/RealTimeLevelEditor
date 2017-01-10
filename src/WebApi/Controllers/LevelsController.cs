using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.ViewModels.Levels;
using WebApi.Services;

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
			string userId = GetUserId();
			var result = _levelLoader.Create(userId, levelViewModel.Name);

			return Created(GetLevelUri(result.Info), result.Info);
		}


		private string GetLevelUri(LevelInfoViewModel level)
		{
			return $"{uri}/{level.LevelId}";
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
