namespace WebApi.Controllers {

export class EditTileTypesController {

    public projectId;
    public projectName;

    public tileTypes;

    public newTypeName;
    public newTypeURL: string;
    public displayURL;

    getTileTypes() {

        this.$http.get('api/types/' + this.projectId).then((res) => {
            this.tileTypes = res.data;
        });

    }

    addTileType() {

        if (this.newTypeURL.length > 25) {
            let l = this.newTypeURL.length;
            this.displayURL = "..." + this.newTypeURL.substring(l - 25, l - 1);
        }
        else {
            this.displayURL = this.newTypeURL;
        }
        this.tileTypes.push({ name: this.newTypeName, tileModel: this.newTypeURL, displayUrl: this.displayURL });


        let data = { tileModel: this.newTypeURL, name: this.newTypeName };


        this.$http.post(`api/types/${this.projectId}`, data);


    }


	constructor(
		private $http: ng.IHttpService,
	private $stateParams: ng.ui.IStateParamsService) {
		this.projectId = $stateParams[ParamNames.projectId];
		this.projectName = $stateParams[ParamNames.projectName];
        this.getTileTypes();
    }

}
}