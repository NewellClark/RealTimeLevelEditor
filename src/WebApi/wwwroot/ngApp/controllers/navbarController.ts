namespace WebApi.Controllers {

	export class NavbarLink {
		public constructor(
			private _displayName: string,
			private path: string,
			private $location: ng.ILocationService) { }

		public isVisible(account: AccountController): boolean {
			return true;
		}

		public navigate(): void {
			this.$location.path(this.path);
		}

		public displayName(): string {
			return this._displayName;
		}
	}

	class RequiresLoggedInLink extends NavbarLink {
		public isVisible(account: AccountController): boolean {
			let result: boolean = false;
			if (account.isLoggedIn())
				result = true;

			return result;
		}
	}

	class RequiresLoggedOutLink extends NavbarLink {
		public isVisible(account: AccountController): boolean {
			let result = true;
			if (account.isLoggedIn())
				result = false;

			return result;
		}
	}

	export class NavbarController {
		constructor(
			private $location: ng.ILocationService,
			private $http: ng.IHttpService) {
			//this.projects = new RequiresLoggedInLink("Projects", "/projects", $location);
			//this.register = new RequiresLoggedOutLink("Register", "/register", $location);
			this.links = [];
			this.projects = this.addLink(new RequiresLoggedInLink("Projects", "/projects", $location));
			this.register = this.addLink(new RequiresLoggedOutLink("Register", "/register", $location));
		}

		public readonly links: NavbarLink[];

		public readonly projects: NavbarLink;

		public readonly register: NavbarLink;

		public readonly showDisabledLinks: boolean = true;

		private addLink(link: NavbarLink): NavbarLink {
			this.links.push(link);
			return link;
		}
	}
	angular.module("WebApi").controller("NavbarController", NavbarController);

}