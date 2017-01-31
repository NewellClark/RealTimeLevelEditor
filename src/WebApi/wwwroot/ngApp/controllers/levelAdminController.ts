namespace WebApi.Controllers {

export class LevelAdminController {

	public projectId;
	public projectName;
	public levelRoster;
	public levelName;

	public getLevels() {
		this.$http.get('/api/projects/' + this.projectId + '/levels').then((res) => {
			this.levelRoster = res.data;
		});
	}

	public addLevel() {
		let data = { Name: this.levelName };
		this.$http.post('/api/levels/' + this.projectId, data).then(() => {
			this.getLevels();
		});
	}

	public editLevel(index) {
		//localStorage.setItem("levelId", this.levelRoster[index].levelId);
		//localStorage.setItem("levelName", this.levelRoster[index].name);
		//this.$location.path('/level');
		let params = {
			id: this.levelRoster[index].levelId
		}
		this.$state.go(States.level, params);

	}

	public deleteLevel(index) {
		let theLevelId = this.levelRoster[index].levelId;
		this.$http.delete('/api/levels/' + theLevelId).then(() => {
			this.getLevels();
		});

	}

	public gotoEditTileTypes() {
		this.$location.path('/editTileTypes');
	}

	public downloadLevel(index) {
		let levelId = this.levelRoster[index].levelId;
		let url = `/api/levels/${levelId}/download`;

		this.$location.path(url);
		let element = document.createElement("a");
		element.setAttribute("href", url);
		element.setAttribute("target", "blank");
		element.click();
	}

	constructor(
		private $http: ng.IHttpService,
		private $location: ng.ILocationService,
		private $state: ng.ui.IStateService,
		private $stateParams: ng.ui.IStateParamsService) {
		this.projectId = localStorage.getItem("projectId");
		this.projectName = localStorage.getItem("projectName");
		this.getLevels();
	}

}
}