namespace WebApi.Controllers {

export class LevelNonAdminController {

    public projectId;
    public projectName;
    public levelRoster;
    public levelName;

    getLevels() {
        this.$http.get('/api/projects/' + this.projectId + '/levels').then((res) => {
            this.levelRoster = res.data;
        });
    }

    addLevel() {
        let data = { Name: this.levelName };
        this.$http.post('/api/levels/' + this.projectId, data).then(() => {
            this.getLevels();
        });
    }

    editLevelNonAdmin(index) {
        //localStorage.setItem("levelId", this.levelRoster[index].levelId);
        //localStorage.setItem("levelName", this.levelRoster[index].name);
        //this.$location.path('/level');
		let params = new LevelParams(
			this.projectId,
			this.projectName,
			this.levelRoster[index].levelId,
			this.levelRoster[index].name);
		this.$state.go(States.level, params);
    }

    gotoEditTileTypes() {

        //this.$location.path('/editTileTypes');
		this.$state.go(States.editTileTypes);
    }

	constructor(
		private $http: ng.IHttpService,
		private $location: ng.ILocationService,
		private $state: ng.ui.IStateService,
		private $stateParams: ng.ui.IStateParamsService) {
        //this.projectId = localStorage.getItem("projectId");
        //this.projectName = localStorage.getItem("projectName");
		this.projectId = $stateParams[LevelParamNames.projectId];
		this.projectName = $stateParams[LevelParamNames.projectName];
        this.getLevels();
    }

}
}