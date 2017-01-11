namespace WebApi.Controllers {

    export class HomeController {


        public anchorX = 0;
        public anchorY = 0;
        public anchoring = false;
        public mapOffSetX = 0;
        public mapOffSetY = 0;

        public mapPositionX = 0;
        public mapPositionY = 0;

        public selecting = false;
        public selectAnchoring = false;
        public selectX0 = 0;
        public selectY0 = 0;
        public selectX1 = 0;
        public selectY1 = 0;

        public BMPIndex = 0;

        public addToSelect;

        public tileImagesData = [];
       

        public scroll = false;
        public numTiles = 20;
        public scaleFactor = 1;
        public hasSelection = false;

        public typesMenuVisible = true;
        public propertyToAdd = "";

        public colorToAdd = "#ff0000";

        public types = [
        {color: "#ffff00", name: "Yellow"},
        {color: "#00ff00", name: "Green"},
        {color: "#ff0000", name: "Fire" },
        {color: "#0000ff", name: "Water" }
        ];

        public tileImages = [
            { id: 0, name: "-------------"},
            { id: 1, name: "tilemario.png"},
            { id: 2, name: "tilegoomba.png" },
            { id: 3, name: "tilesky.png" },
            { id: 4, name: "tilebrick.png" },
            { id: 5, name: "tileblock.png" },
            { id: 6, name: "tileplantleft.png" },
            { id: 7, name: "tileplantmiddle.png" },
            { id: 8, name: "tileplantright.png" },
            { id: 9, name: "tileitem.png" },
            { id: 10, name: "tileground.png" }
        ];

        public tileImagesFiles = [
            "-------------" ,
            "tilemario.png" ,
            "tilegoomba.png" ,
            "tilesky.png" ,
            "tilebrick.png" ,
            "tileblock.png" ,
            "tileplantleft.png" ,
            "tileplantmiddle.png" ,
            "tileplantright.png" ,
            "tileitem.png",
            "tileground.png" 
        ];

        public selectedTiles = [];

        public fireType = { name: "Fire", color: "#ff0000",
            properties: [{ property: "Harm", value: 10 },
                { property: "CanWalk", value: "false" }
            ]};

        public Region = {
            Dimensions: {
                Left: 0,
                Top: 0,
                Width: 20,
                Height: 20
            },

            Tiles:
            [
                {
                    Index: { X: 1, Y: 5 },
                    Data: { color: "#005500", type: "block", imageInfo: null }
                },
                {
                    Index: { X: 3, Y: 9 },
                    Data: { color: "#000099", type: "block", imageInfo: null }
                },
                {
                    Index: { X: 10, Y: 18 },
                    Data: { color: "#ff0000", type: "block", imageInfo: null }
                }
            ]
        };

        ToggleSelect() {
            this.selecting = !this.selecting;
            if (this.selecting == true) {
                this.scroll = false;
            }
        }

        ToggleDraw() {
            this.selecting = false;
            this.anchoring = false;
            this.selectAnchoring = false;
            this.scroll = false;
        }

        RenderCanvas() {

            let canvas = <HTMLCanvasElement>document.getElementById('theCanvas');
            let canvasContext: CanvasRenderingContext2D = canvas.getContext('2d');

            this.getScaleFactor();
            let td = this.scaleFactor;
            

          for (let i = 0; i < this.Region.Tiles.length; i++) {
             let tx = td * this.Region.Tiles[i].Index.X + this.mapPositionX + this.mapOffSetX;
             let ty = td * this.Region.Tiles[i].Index.Y + this.mapPositionY + this.mapOffSetY;

             canvasContext.fillStyle = this.Region.Tiles[i].Data.color;
             if (this.Region.Tiles[i].Data.imageInfo == null) {
                 canvasContext.fillRect(tx, ty, td, td);
             }
             else
             {
                 let j = this.Region.Tiles[i].Data.imageInfo; 
                 canvasContext.drawImage(this.tileImagesData[j], tx, ty, td, td);
                 //let img1 = new Image();
                 //img1.onload = function () {
                 //    canvasContext.drawImage(img1, tx, ty, td, td);
                 //};
                 //img1.src = "../../images/" + this.Region.Tiles[i].Data.imageInfo; 
             }
            }

          if (this.selectAnchoring == true) {
              canvasContext.setLineDash([5, 15]);
              canvasContext.rect(this.selectX0, this.selectY0,
                  this.selectX1-this.selectX0, this.selectY1-this.selectY0);
              canvasContext.stroke();
          }

          if (this.hasSelection == true)
              this.drawSelection();
        }

        addTile(pixelX, pixelY) {

            let select = <HTMLSelectElement>document.getElementById("repeatSelect");

            let imageInfo = null;
            if (select.selectedIndex > 0) {
                imageInfo = select.selectedIndex;//this.tileImagesFiles[select.selectedIndex];
                let x = 0;
            }

            let blockX = Math.floor(pixelX / this.scaleFactor);
            let blockY = Math.floor(pixelY / this.scaleFactor);

            this.Region.Tiles.push({
                Index: { X: blockX, Y: blockY },
                Data: { color: this.colorToAdd, type: "block", imageInfo: imageInfo }
            });
        }

        removeTile(pixelX, pixelY) {
            let blockX = Math.floor(pixelX / this.scaleFactor);
            let blockY = Math.floor(pixelY / this.scaleFactor);
            for (let i = 0; i < this.Region.Tiles.length; i++) {
                if (this.Region.Tiles[i].Index.X == blockX &&
                    this.Region.Tiles[i].Index.Y == blockY) {
                    this.Region.Tiles.splice(i, 1);;
                }
            }

        }

        selectTiles() {
           let canvas = <HTMLCanvasElement>document.getElementById('theCanvas');

           let pixelX0 = this.selectX0 - canvas.offsetLeft - this.mapPositionX;
           let pixelY0 = this.selectY0 - canvas.offsetTop - this.mapPositionY;
           let pixelX1 = this.selectX1 - canvas.offsetLeft - this.mapPositionX;
           let pixelY1 = this.selectY1 - canvas.offsetTop - this.mapPositionY;

           pixelX0 /= this.scaleFactor; pixelX1 /= this.scaleFactor;
           pixelY0 /= this.scaleFactor; pixelY1 /= this.scaleFactor;

           if (this.addToSelect == false) this.selectedTiles = [];

           for (let i = 0; i < this.Region.Tiles.length; i++) {
               if (this.Region.Tiles[i].Index.X >= pixelX0 && 
                   this.Region.Tiles[i].Index.Y >= pixelY0 &&
                   this.Region.Tiles[i].Index.X <= pixelX1 &&
                   this.Region.Tiles[i].Index.Y <= pixelY1) {
                   this.selectedTiles.push({
                       X: this.Region.Tiles[i].Index.X,
                       Y: this.Region.Tiles[i].Index.Y,
                       index: i
                   });
               }
           }
           this.hasSelection = true;

        }

        Scroll() {
            this.selecting = false;
            this.scroll = !this.scroll;
        }

        mouseDown(event) {
            let canvas = <HTMLCanvasElement>document.getElementById('theCanvas');

            if (this.selecting == true) {
                this.selectX0 = event.x;
                this.selectY0 = event.y;
                this.selectX1 = event.x + 1;
                this.selectY1 = event.y + 1;
                this.selectAnchoring = true;
                return;
            }

            if (this.scroll == false) {


                if (event.button == 0) {
                    this.addTile(event.x - canvas.offsetLeft - this.mapPositionX, event.y - canvas.offsetTop - this.mapPositionY);
                    this.RenderCanvas();
                }
                if (event.button == 2) {
                    this.removeTile(event.x - canvas.offsetLeft - this.mapPositionX, event.y - canvas.offsetTop - this.mapPositionY);
                    this.RenderCanvas();
                }
             }
            else
            {
                    this.setAnchorPoint(event.x - canvas.offsetLeft, event.y - canvas.offsetTop);
                    this.anchoring = true;                
            }
        }

        mouseMove(event) {

            let canvas = <HTMLCanvasElement>document.getElementById('theCanvas');

            if (this.anchoring == true)
                this.moveByAnchor(event.x - canvas.offsetLeft, event.y - canvas.offsetTop);
            if (this.selecting == true && this.selectAnchoring == true) {
                this.selectX1 = event.x;
                this.selectY1 = event.y;
            }

            this.RenderCanvas();
        }

        mouseUp(event) {
            if (this.anchoring && this.Scroll) {
                this.mapPositionX += this.mapOffSetX;
                this.mapPositionY += this.mapOffSetY;
                this.mapOffSetX = 0;
                this.mapOffSetY = 0;
                this.anchorX = 0;
                this.anchorY = 0;

                this.anchoring = false;
                
            }

            if (this.selecting == true) {

                this.selectTiles();
                this.selectAnchoring = false;
                //this.hasSelection = false;

                this.RenderCanvas();
                

            }

            }

        drawSelection() {

            if (this.hasSelection == false) return;

            let canvas = <HTMLCanvasElement>document.getElementById('theCanvas');
            let canvasContext: CanvasRenderingContext2D = canvas.getContext('2d');

            let td = this.scaleFactor;

            for (let i = 0; i < this.selectedTiles.length; i++) {

                let tx = td * this.selectedTiles[i].X + this.mapPositionX + this.mapOffSetX;
                let ty = td * this.selectedTiles[i].Y + this.mapPositionY + this.mapOffSetY;

                canvasContext.setLineDash([5, 15]);
                canvasContext.rect(tx, ty, td, td);
                canvasContext.stroke();

            }



        }

        setAnchorPoint(x, y) {

            this.anchorX = x;
            this.anchorY = y;

        }

        resizeWindow() {
            this.getScaleFactor();
        }

        moveByAnchor(x, y) {

            this.mapOffSetX = x - this.anchorX;
            this.mapOffSetY = y - this.anchorY;
            this.RenderCanvas();
        }

        getScaleFactor() {

            let td = 1;
            let canvas = <HTMLCanvasElement>document.getElementById('theCanvas');
            canvas.width = window.innerWidth;
            canvas.height = window.innerHeight;

            if (canvas.height < canvas.width) { td = canvas.height / this.numTiles }
            else { td = canvas.width / this.numTiles; }
            this.scaleFactor = td;
            //this.RenderCanvas();

        }

        addProperty() {
            this.fireType.properties.push({ property: this.propertyToAdd, value: "" });
        }

        loadImageTiles() {

            for (let i = 0; i < this.tileImagesFiles.length; i++) {
                this.tileImagesData.push(new Image());
                this.tileImagesData[i].src = "../../images/" + this.tileImagesFiles[i];
            }
           // let img1 = new Image();
           // img1.src = "../../images/" + this.Region.Tiles[i].Data.imageInfo; 

        }

        public keyDown = (evt: KeyboardEvent) =>  {

           

            if (evt.ctrlKey == true) {
                this.addToSelect = true;
            }

            if (evt.keyCode == 46) {
                this.handleDelete();
            }
        }

        handleDelete() {
            let td = this.scaleFactor;
            for (let i = 0; i < this.selectedTiles.length; i++) {
                this.removeTile(this.selectedTiles[i].X * td,
                    this.selectedTiles[i].Y * td);
            }

            this.selectedTiles = [];
            this.RenderCanvas();
        }

        handleBMPSelect() {

            if (this.selectedTiles != []) {

                for (let i = 0; i < this.selectedTiles.length; i++) {

                    this.Region.Tiles[this.selectedTiles[i].index].Data.imageInfo = this.BMPIndex;

                }

            }

            this.RenderCanvas();

        }

        public keyUp = (evt: KeyboardEvent) => {
            if (evt.ctrlKey == false) this.addToSelect = false;
        }

        constructor() {
            this.getScaleFactor();
            this.addToSelect = false;

            this.addTile(0, 50);
            this.loadImageTiles();
            this.RenderCanvas();

            window.addEventListener("resize", this.getScaleFactor);
            window.addEventListener("keydown", this.keyDown);
            window.addEventListener("keyup", this.keyUp);

     }

    }


    export class SecretController {
        public secrets;

        constructor($http: ng.IHttpService) {
            $http.get('/api/secrets').then((results) => {
                this.secrets = results.data;
            });
        }
    }


    export class AboutController {
        public message = 'Hello from the about page!';
    }

}
