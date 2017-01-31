namespace WebApi.Controllers {

	export class CreateTeamController {
		public projectId;
		public projectName;

		public teamMemberEmail;
		public userProjects;
		public userEmail;
		public teamRoster;

		getTeamMembers() {
			this.$http.get(`/api/members/${this.projectId}/getusers`).then((res) => {
				this.teamRoster = res.data;
			});
		}

		addTeamMember() {

			this.$http.post(`/api/projects/${this.projectId}/${this.teamMemberEmail}`, "name").then(() => {
				this.getTeamMembers();
			});
		}

		constructor(
			private $http: ng.IHttpService,
		private $stateParams: ng.ui.IStateParamsService) {
			//this.projectId = localStorage.getItem("projectId");
			//this.projectName = localStorage.getItem("projectName");
			this.projectId = $stateParams[ParamNames.projectId];
			this.projectName = $stateParams[ParamNames.projectName];
			this.getTeamMembers();
		}
	}
}