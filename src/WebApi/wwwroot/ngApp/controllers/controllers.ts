namespace WebApi.Controllers {

	export class HomeController {

		//////////////////////////////
		//Level information
		public projectId;
		public projectName;

		public levelId;
		public levelName;

		/////////////////////////////
		//Tile types for tile type selector    
		public tileTypes = [];

		//Toggle scrolling
		public scroll = false;
	  

		/////////////////////////
		//Tile type image files
		public tileImages = [];
	   
		public tileImagesFiles = [];
	   
		public selectedTiles = [];


		//////////////////////////////////
		//Rendering code

		RenderCanvas() {

			this.homeService.RenderCanvas();
		}

		RenderCanvas20() {

			this.RenderCanvas20();

		}
			  
		//////////////////////////////////
		//Action buttons
				
		Scroll() {

			this.homeService.Scroll();

		}

		ToggleSelect() {

			this.homeService.ToggleSelect();

		}

		ToggleDraw() {

			this.homeService.ToggleDraw();

		}

		//////////////////////////////////
		//Mouse events

		mouseDown(event) {

			this.homeService.mouseDown(event);

		}

		mouseMove(event) {

			this.homeService.mouseMove(event);

		}

		mouseUp(event) {

			this.homeService.mouseUp(event);

		}

		//////////////////////////////////
		//Keyboard events

		public keyDown = (evt: KeyboardEvent) => {

			this.homeService.keyDown(evt);

		}


		public keyUp = (evt: KeyboardEvent) => {

			this.homeService.keyUp(evt);

		}

		//drawSelection() {

		//    this.homeService.drawSelection();

		//}

		setAnchorPoint(x, y) {

			this.homeService.setAnchorPoint(x, y);

		}

		resizeWindow() {

			this.homeService.resizeWindow();

		}

		moveByAnchor(x, y) {

			this.homeService.moveByAnchor(x, y);

		}

		getScaleFactor() {

			this.homeService.getScaleFactor();

		}
		
		//////////////////////////////
		//Chunk 2.0 Code

		public loadRegionObjects(x, y, w, h) {

			this.homeService.loadRegionObjects(x, y, w, h);

		}

		//////////////////////////////
		//Type code

		public testType() {

			this.homeService.testType();

		}


		public loadTypes() {

			this.$http.get(`api/types/${this.projectId}`).then((res) => {
				let types = <WebApi.Controllers.DTOType[]>res.data;

				console.log(types);
				this.tileTypes = types;

				this.tileImages = []; this.tileImagesFiles = [];

				this.tileImages.push({ id: 0, name: "---------" });
				this.tileImagesFiles.push("--------");
				for (let i = 0; i < types.length; i++) {
					this.tileImages.push({ id: i + 1, name: types[i].name });
					this.tileImagesFiles.push(types[i].tileModel);
				}

			});

		}


		loadProperty(name) {

			this.homeService.loadProperty(name);

		}

		//////////////////////////////
		////Chunk loading/unloading code

			constructor(private homeService: WebApi.Services.HomeService,
				private $http: ng.IHttpService,
			private $stateParams: ng.ui.IStateParamsService) {

			//this.levelId = localStorage.getItem("levelId");
				this.levelId = $stateParams["id"];
			this.levelName = localStorage.getItem("levelName");
			this.projectId = localStorage.getItem("projectId");
			this.projectName = localStorage.getItem("projectName");

			
	 
			this.loadTypes();

		}

	}

	angular.module('WebApi').controller('homeController', HomeController);
}
