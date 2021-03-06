namespace WebApi.Controllers {

	export class AccountController {

		public externalLogins;

		public getUserName() {
			return this.accountService.getUserName();
		}

		public getClaim(type) {
			return this.accountService.getClaim(type);
		}

		public isLoggedIn() {
			return this.accountService.isLoggedIn();
		}

		public logout() {
			this.accountService.logout();
			//this.$location.path('/');
			this.$state.go(States.home);
		}

		public getExternalLogins() {
			return this.accountService.getExternalLogins();
		}

		constructor(
			private accountService: WebApi.Services.AccountService,
			private $state: ng.ui.IStateService,
			private $location: ng.ILocationService) {
			this.getExternalLogins().then((results) => {
				this.externalLogins = results;
			});
		}
	}

	angular.module('WebApi').controller('AccountController', AccountController);


	export class LoginController {
		public loginUser;
		public validationMessages;

		public gotoProjects() {
			//this.$location.path('/projects')
			this.$state.go(States.projects);
		}

		public login() {
			this.accountService.login(this.loginUser).then(() => {
				//this.$location.path('/');
				this.$state.go(States.home);
			}).catch((results) => {
				this.validationMessages = results;
			});
		}

		constructor(
			private accountService: WebApi.Services.AccountService,
			private $location: ng.ILocationService,
			private $state: ng.ui.IStateService) { }
	}


	export class RegisterController {
		public registerUser;
		public validationMessages: string[] = [];

		public showErrors(): boolean {
			return this.validationMessages.length > 0;
		}

		public register() {
			this.accountService.register(this.registerUser).then(() => {
				//this.$location.path('/');
				this.$state.go(States.home);
			}).catch((results) => {
				this.validationMessages = results;
			});
		}

		constructor(
			private accountService: WebApi.Services.AccountService,
			private $location: ng.ILocationService,
			private $state: ng.ui.IStateService) { }
	}





	export class ExternalRegisterController {
		public registerUser;
		public validationMessages;

		public register() {
			this.accountService.registerExternal(this.registerUser.email)
				.then((result) => {
					//this.$location.path('/');
					this.$state.go(States.home);
				}).catch((result) => {
					this.validationMessages = result;
				});
		}

		constructor(
			private accountService: WebApi.Services.AccountService,
			private $location: ng.ILocationService,
			private $state: ng.ui.IStateService) { }

	}

	export class ConfirmEmailController {
		public validationMessages;

		constructor(
			private accountService: WebApi.Services.AccountService,
			private $http: ng.IHttpService,
			private $stateParams: ng.ui.IStateParamsService,
			private $location: ng.ILocationService,
			private $state: ng.ui.IStateService
		) {
			let userId = $stateParams['userId'];
			let code = $stateParams['code'];
			accountService.confirmEmail(userId, code)
				.then((result) => {
					//this.$location.path('/');
					this.$state.go(States.home);
				}).catch((result) => {
					this.validationMessages = result;
				});
		}
	}

}
