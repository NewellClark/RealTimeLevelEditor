namespace WebApi.Controllers {

export class LevelAdminController {

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

    editLevel(index) {
        localStorage.setItem("levelId", this.levelRoster[index].levelId);
        localStorage.setItem("levelName", this.levelRoster[index].name);
        this.$location.path('/level');

    }

    deleteLevel(index) {
        let theLevelId = this.levelRoster[index].levelId;
        this.$http.delete('/api/levels/' + theLevelId).then(() => {
            this.getLevels();
        });

    }

    gotoEditTileTypes() {

        this.$location.path('/editTileTypes');

    }


    constructor(private $http: ng.IHttpService, private $location: ng.ILocationService) {
        this.projectId = localStorage.getItem("projectId");
        this.projectName = localStorage.getItem("projectName");
        this.getLevels();
    }

}
}