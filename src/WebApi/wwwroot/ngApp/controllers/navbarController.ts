namespace WebApi.Controllers {

	class ActionArgs {
		constructor(
			public readonly $location: ng.ILocationService,
			public readonly $state: ng.ui.IStateService) { }
	}
	export class NavbarAction {
		public constructor(
			private _displayName: string,
			public readonly path: string,
			args: ActionArgs) {
			this.$location = args.$location;
			this.$state = args.$state;
		}

		protected readonly $location: ng.ILocationService;
		protected readonly $state: ng.ui.IStateService;

		public isVisible(account: AccountController): boolean {
			return true;
		}

		public execute(account: AccountController): void {
			//this.$location.path(this.path);
			this.$state.go(this.path);
		}

		public displayName(): string {
			return this._displayName;
		}
	}

	class RequiresLoggedInAction extends NavbarAction {
		public isVisible(account: AccountController): boolean {
			let result: boolean = false;
			if (account.isLoggedIn()) {
				result = true;
			}
			return result;
		}
	}

	class RequiresLoggedOutAction extends NavbarAction {
		public isVisible(account: AccountController): boolean {
			let result = true;
			if (account.isLoggedIn()) {
				result = false;
			}
			return result;
		}
	}

	class LogoutAction extends RequiresLoggedInAction {
		public execute(account: AccountController): void {
			account.logout();
			super.execute(account);
		}
	}

	export class NavbarController {
		constructor(
			private $location: ng.ILocationService,
			private $http: ng.IHttpService,
		private $state: ng.ui.IStateService) {
			this.actions = [];
			//this.home = this.addAction(new NavbarAction("Home", "/", $location));
			//this.projects = this.addAction(new RequiresLoggedInAction("Projects", "/projects", $location));
			//this.logout = this.addAction(new LogoutAction("Logout", this.home.path, $location));
			//this.login = this.addAction(new RequiresLoggedOutAction("Login", this.home.path, $location));
			//this.register = this.addAction(new RequiresLoggedOutAction("Register", "/register", $location));
			let args = new ActionArgs($location, $state);
			this.home = this.addAction(new NavbarAction("Home", "home", args));
			this.projects = this.addAction(new RequiresLoggedInAction("Projects", "projects", args));
			this.login = this.addAction(new RequiresLoggedOutAction("Login", "login", args));
			this.logout = this.addAction(new LogoutAction("Logout", this.home.path, args));
			this.register = this.addAction(new RequiresLoggedOutAction("Register", "register", args));
		}

		public readonly actions: NavbarAction[];

		public readonly projects: NavbarAction;

		public readonly register: NavbarAction;

		public readonly home: NavbarAction;

		public readonly login: NavbarAction;

		public readonly logout: NavbarAction;

		public readonly showDisabledLinks: boolean = true;

		private addAction(action: NavbarAction): NavbarAction {
			this.actions.push(action);
			return action;
		}
	}
	angular.module("WebApi").controller("NavbarController", NavbarController);

}