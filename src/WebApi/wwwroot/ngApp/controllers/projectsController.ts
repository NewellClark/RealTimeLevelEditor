namespace WebApi.Controllers {

    export class ProjectsController {
        public userProjects;
        public memberName;
        public projectName;
        public newProjectName: string;
        public nonAdmin;

        public userId;

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
            localStorage.setItem("projectId", this.userId[index].projectId);
            localStorage.setItem("projectName", this.userId[index].projectName);
            this.$location.path('/createTeam');
        }

        viewLevels(index) {
            localStorage.setItem("projectId", this.userId[index].projectId);
            localStorage.setItem("projectName", this.userId[index].projectName);
            this.$location.path('/levelAdmin');
        }

        viewNonAdminLevels(index) {
            localStorage.setItem("projectId", this.nonAdmin[index].projectId);
            localStorage.setItem("projectName", this.nonAdmin[index].projectName);
            this.$location.path('/levelNonAdmin');
        }

        deleteProject(index) {
            let theProjectId = this.userId[index].projectId;
            this.$http.delete('/api/projects/' + theProjectId).then(() => {
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

        constructor(private $http: ng.IHttpService, private $location: ng.ILocationService) {
            this.loadProjectList();
            this.loadNonAdminList();
        }

    }
}