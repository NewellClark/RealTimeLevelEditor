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
		localStorage.setItem("levelId", this.levelRoster[index].levelId);
		localStorage.setItem("levelName", this.levelRoster[index].name);
		this.$location.path('/level');

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
		console.log("downloadLevel(index) not implimented yet :/");
	}

	constructor(private $http: ng.IHttpService, private $location: ng.ILocationService) {
		this.projectId = localStorage.getItem("projectId");
		this.projectName = localStorage.getItem("projectName");
		this.getLevels();
	}

}
}