namespace WebApi {
	//	From now on, define magic strings here.

	//	State params for "level" state.
	export class ParamNames {
		public static readonly levelId: string = "levelId";
		public static readonly projectId: string = "projectId";
		public static readonly projectName: string = "projectName";
		public static readonly levelName: string = "levelName";
	}

	//	Pass this into $state.go() for the "params" parameter when
	//		navigating to the level editing page.
	export class LevelParams {
		public constructor(
			public readonly projectId: string,
			public readonly projectName: string,
			public readonly levelId: string,
			public readonly levelName: string) {

		}
	}

	//	pass this into $state.go() for the "params" parameter
	//		when navigating to page that shows a single project.
	export class ProjectParams {
		public constructor(
			public readonly projectId: string,
			public readonly projectName: string) { }
	}

	export class States {
		public static readonly home: string = "home";
		public static readonly level: string = "level";
		public static readonly login: string = "login";
		public static readonly register: string = "register";
		public static readonly createTeam: string = "createTeam";
		public static readonly projects: string = "projects";
		public static readonly levelAdmin: string = "leveladmin";
		public static readonly levelNonAdmin: string = "levelnonadmin";
		public static readonly editTileTypes: string = "editTileTypes";
		public static readonly notFound: string = "notFound";
	}

	angular.module('WebApi', ['ui.router', 'ngResource', 'ui.bootstrap']).config((
		$stateProvider: ng.ui.IStateProvider,
		$urlRouterProvider: ng.ui.IUrlRouterProvider,
		$locationProvider: ng.ILocationProvider
	) => {
		// Define routes
		$stateProvider
			.state(States.home, {
				url: "/",
				templateUrl: "/ngApp/views/home.html"
			})
			.state(States.level, {
				url: `/level/:${ParamNames.projectId}/:${ParamNames.projectName}/:${ParamNames.levelId}/:${ParamNames.levelName}`,
				templateUrl: '/ngApp/views/level.html',
				controller: WebApi.Controllers.HomeController,
				controllerAs: 'controller'
			})
			.state(States.login, {
				url: '/login',
				templateUrl: '/ngApp/views/login.html',
				controller: WebApi.Controllers.LoginController,
				controllerAs: 'controller'
			})
			.state(States.register, {
				url: '/register',
				templateUrl: '/ngApp/views/register.html',
				controller: WebApi.Controllers.RegisterController,
				controllerAs: 'controller'
			})
			.state(States.createTeam, {
				url: `/createTeam/:${ParamNames.projectId}/:${ParamNames.projectName}`,
				templateUrl: '/ngApp/views/createTeam.html',
				controller: WebApi.Controllers.CreateTeamController,
				controllerAs: 'controller'
			})
			.state(States.projects, {
				url: '/projects',
				templateUrl: '/ngApp/views/projects.html',
				controller: WebApi.Controllers.ProjectsController,
				controllerAs: 'controller'
			})
			.state(States.levelAdmin, {
				url: `/levelAdmin/:${ParamNames.projectId}/:${ParamNames.projectName}`,
				templateUrl: '/ngApp/views/levelAdmin.html',
				controller: WebApi.Controllers.LevelAdminController,
				controllerAs: 'controller'
			})
			.state(States.levelNonAdmin, {
				url: `/levelNonAdmin/:${ParamNames.projectId}/:${ParamNames.projectName}`,
				templateUrl: '/ngApp/views/levelNonAdmin.html',
				controller: WebApi.Controllers.LevelNonAdminController,
				controllerAs: 'controller'
			})
			.state(States.editTileTypes, {
				url: `/editTileTypes/:${ParamNames.projectId}/:${ParamNames.projectName}`,
				templateUrl: '/ngApp/views/editTileTypes.html',
				controller: WebApi.Controllers.EditTileTypesController,
				controllerAs: 'controller'
			})
			.state(States.notFound, {
				url: '/notFound',
				templateUrl: '/ngApp/views/notFound.html'
			});

		// Handle request for non-existent route
		$urlRouterProvider.otherwise('/notFound');

		// Enable HTML5 navigation
		$locationProvider.html5Mode(true);
	});

	
	angular.module('WebApi').factory('authInterceptor', (
		$q: ng.IQService,
		$window: ng.IWindowService,
		$location: ng.ILocationService
	) =>
		({
			request: function (config) {
				config.headers = config.headers || {};
				config.headers['X-Requested-With'] = 'XMLHttpRequest';
				return config;
			},
			responseError: function (rejection) {
				if (rejection.status === 401 || rejection.status === 403) {
					$location.path('/');
				}
				return $q.reject(rejection);
			}
		})
	);

	angular.module('WebApi').config(function ($httpProvider) {
		$httpProvider.interceptors.push('authInterceptor');
	});

	

}
