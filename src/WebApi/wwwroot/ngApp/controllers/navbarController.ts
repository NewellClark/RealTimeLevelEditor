namespace WebApi.Controllers {

	export class NavbarAction {
		public constructor(
			private _displayName: string,
			public readonly path: string,
			protected $location: ng.ILocationService) { }

		public isVisible(account: AccountController): boolean {
			return true;
		}

		public execute(account: AccountController): void {
			this.$location.path(this.path);
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
		}
	}

	export class NavbarController {
		constructor(
			private $location: ng.ILocationService,
			private $http: ng.IHttpService) {
			this.actions = [];
			this.home = this.addAction(new NavbarAction("Home", "/", $location));
			this.projects = this.addAction(new RequiresLoggedInAction("Projects", "/projects", $location));
			this.logout = this.addAction(new LogoutAction("Logout", this.home.path, $location));
			this.login = this.addAction(new RequiresLoggedOutAction("Login", this.home.path, $location));
			this.register = this.addAction(new RequiresLoggedOutAction("Register", "/register", $location));
		}

		public readonly actions: NavbarAction[];

		public readonly projects: NavbarAction;

		public readonly register: NavbarAction;

		public readonly home: NavbarAction;

		public readonly login: NavbarAction;

		public readonly logout: NavbarAction;

		public readonly showDisabledLinks: boolean = true;

		public isVisible(): boolean {

		}

		private addAction(action: NavbarAction): NavbarAction {
			this.actions.push(action);
			return action;
		}
	}
	angular.module("WebApi").controller("NavbarController", NavbarController);

}