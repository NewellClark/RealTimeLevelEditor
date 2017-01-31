namespace WebApi {

	angular.module('WebApi', ['ui.router', 'ngResource', 'ui.bootstrap']).config((
		$stateProvider: ng.ui.IStateProvider,
		$urlRouterProvider: ng.ui.IUrlRouterProvider,
		$locationProvider: ng.ILocationProvider
	) => {
		// Define routes
		$stateProvider
			.state('level', {
                url: '/level/:levelId/:levelName/:projectId/:projectName',
				templateUrl: '/ngApp/views/home.html',
				controller: WebApi.Controllers.HomeController,
				controllerAs: 'controller'
			})
			.state('login', {
				url: '/',
				templateUrl: '/ngApp/views/login.html',
				controller: WebApi.Controllers.LoginController,
				controllerAs: 'controller'
			})
			.state('register', {
				url: '/register',
				templateUrl: '/ngApp/views/register.html',
				controller: WebApi.Controllers.RegisterController,
				controllerAs: 'controller'
			})
			.state('createTeam', {
				url: '/createTeam',
				templateUrl: '/ngApp/views/createTeam.html',
				controller: WebApi.Controllers.CreateTeamController,
				controllerAs: 'controller'
			})
			.state('projects', {
				url: '/projects',
				templateUrl: '/ngApp/views/projects.html',
				controller: WebApi.Controllers.ProjectsController,
				controllerAs: 'controller'
			})
			.state('leveladmin', {
				url: '/levelAdmin',
				templateUrl: '/ngApp/views/levelAdmin.html',
				controller: WebApi.Controllers.LevelAdminController,
				controllerAs: 'controller'
			})
			.state('levelnonadmin', {
				url: '/levelNonAdmin',
				templateUrl: '/ngApp/views/levelNonAdmin.html',
				controller: WebApi.Controllers.LevelNonAdminController,
				controllerAs: 'controller'
			})
			.state('editTileTypes', {
				url: '/editTileTypes',
				templateUrl: '/ngApp/views/editTileTypes.html',
				controller: WebApi.Controllers.EditTileTypesController,
				controllerAs: 'controller'
			})
			.state('notFound', {
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
