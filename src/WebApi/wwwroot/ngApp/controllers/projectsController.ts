namespace WebApi.Controllers {

	export class ProjectsController {
		public userProjects;
		public memberName;
		public projectName;
		public newProjectName: string;
		public nonAdmin;

		public userId;

		public getProjectId(index): string {
			return this.userId[index].projectId;
		}
		public getProjectName(index): string {
			return this.userId[index].projectName;
		}

		public getNonAdminProjectId(index): string {
			return this.nonAdmin[index].projectId;
		}

		public getNonAdminProjectName(index): string {
			return this.nonAdmin[index].projectName;
		}

		changeView() {

		}

		addProject() {
			let data = this.newProjectName;
			this.$http.post(`/api/projects/${this.newProjectName}`, data).then((res) => {

				let data = <ProjectDTO>res.data;
				let projectId = data.projectId;
				this.loadProjectList();

				let type = { tileModel: "../../images/tileblock.png", name: "block" };
				this.$http.post(`api/types/${projectId}`, type);
				type = { tileModel: "../../images/tilemario.png", name: "mario" };
				this.$http.post(`api/types/${projectId}`, type);
				type = { tileModel: "../../images/tilegoomba.png", name: "goomba" };
				this.$http.post(`api/types/${projectId}`, type);
				type = { tileModel: "../../images/tilebrick.png", name: "brick" };
				this.$http.post(`api/types/${projectId}`, type);
				type = { tileModel: "../../images/tileitem.png", name: "item" };
				this.$http.post(`api/types/${projectId}`, type);
				type = { tileModel: "../../images/tileground.png", name: "ground" };
				this.$http.post(`api/types/${projectId}`, type);


			});
		}

		viewTeams(index) {
			//localStorage.setItem("projectId", this.userId[index].projectId);
			//localStorage.setItem("projectName", this.userId[index].projectName);
			//this.$location.path('/createTeam');
			let params = new ProjectParams(
				this.getProjectId(index),
				this.getProjectName(index));

			this.$state.go(States.createTeam, params);
		}

		viewLevels(index) {
			//localStorage.setItem("projectId", this.userId[index].projectId);
			//localStorage.setItem("projectName", this.userId[index].projectName);
			//this.$location.path('/levelAdmin');

			let params = new ProjectParams(
				this.getProjectId(index),
				this.getProjectName(index));
			this.$state.go(States.levelAdmin, params);
		}

		viewNonAdminLevels(index) {
			//localStorage.setItem("projectId", this.nonAdmin[index].projectId);
			//localStorage.setItem("projectName", this.nonAdmin[index].projectName);
			//this.$location.path('/levelNonAdmin');

			let params = new ProjectParams(
				this.getNonAdminProjectId(index),
				this.getNonAdminProjectName(index));

			this.$state.go(States.levelNonAdmin, params);
		}

		deleteProject(index) {
			let projectId = this.getProjectId(index);
			this.$http.delete('/api/projects/' + projectId).then(() => {
				this.loadProjectList();
			});
		}

		loadProjectList() {
			this.$http.get('/api/projects').then((res) => {
				this.userId = res.data;
			});
		}

		loadNonAdminList() {
			this.$http.get('/api/members').then((res) => {
				this.nonAdmin = res.data;
			});
		}

		constructor(
			private $http: ng.IHttpService,
			private $location: ng.ILocationService,
			private $state: ng.ui.IStateService) {
			this.loadProjectList();
			this.loadNonAdminList();
		}

	}
}