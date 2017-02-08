namespace WebApi.Controllers {

	export class NavbarAction {
		public constructor(
			private _displayName: string,
			public readonly path: string,
			args: ActionArgs) {
			this.$location = args.$location;
			this.$state = args.$state;
		}

		protected readonly navbar: NavbarController;
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

		public isCurrentPage(): boolean {
			return this.$state.current.name == this.path;
		}

		public getLiItemClass(): string {
			if (this.isCurrentPage())
				return "active";
			return "";
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

		public isCurrentPage(): boolean {
			return false;
		}
	}

	class ActionArgs {
		constructor(
			public readonly navbar: NavbarController,
			public readonly $location: ng.ILocationService,
			public readonly $state: ng.ui.IStateService) { }
	}

	//	Allows a class to expose a private property to select external classes.
	class SecretSwitch<T> {
		public get value(): T {
			return this._value;
		}
		public set value(value: T) {
			this._value = value;
		}

		private _value: T;
	}

	export class NavbarController {
		constructor(
			private $location: ng.ILocationService,
			private $http: ng.IHttpService,
			private $state: ng.ui.IStateService) {

			this.actions = [];

			let args = new ActionArgs(this, $location, $state);
			this.home = this.addAction(new NavbarAction("Home", States.home, args));
			this.projects = this.addAction(new RequiresLoggedInAction("Projects", States.projects, args));
			this.login = this.addAction(new RequiresLoggedOutAction("Login", States.login, args));
			this.logout = this.addAction(new LogoutAction("Logout", this.home.path, args));
			this.register = this.addAction(new RequiresLoggedOutAction("Register", States.register, args));
		}

		public readonly actions: NavbarAction[];

		public readonly projects: NavbarAction;

		public readonly register: NavbarAction;

		public readonly home: NavbarAction;

		public readonly login: NavbarAction;

		public readonly logout: NavbarAction;

		public readonly showDisabledLinks: boolean = true;

		public isVisible(): boolean {
			return this.$state.current.name != States.level;
		}

		private addAction(action: NavbarAction): NavbarAction {
			this.actions.push(action);
			return action;
		}
	}
	angular.module("WebApi").controller("NavbarController", NavbarController);

}