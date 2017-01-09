namespace WebApi.Controllers {

    export class HomeController {


        public anchorX = 0;
        public anchorY = 0;
        public anchoring = false;
        public mapOffSetX = 0;
        public mapOffSetY = 0;

        public mapPositionX = 0;
        public mapPositionY = 0;

       

        public scroll = false;
        public numTiles = 20;
        public scaleFactor = 1;

        public typesMenuVisible = true;
        public propertyToAdd = "";

        public types = [
        {color: "#ffff00", name: "Yellow"},
        {color: "#00ff00", name: "Green"},
        {color: "#ff0000", name: "Fire" },
        {color: "#0000ff", name: "Water" }
        ];

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
                    Data: { color: "#005500", type: "block" }
                },
                {
                    Index: { X: 3, Y: 9 },
                    Data: { color: "#000099", type: "block" }
                },
                {
                    Index: { X: 10, Y: 18 },
                    Data: { color: "#ff0000", type: "block" }
                }
            ]
        };


        RenderCanvas() {

            let canvas = <HTMLCanvasElement>document.getElementById('theCanvas');
            let canvasContext: CanvasRenderingContext2D = canvas.getContext('2d');

            this.getScaleFactor();
            let td = this.scaleFactor;
            

          for (let i = 0; i < this.Region.Tiles.length; i++) {
             let tx = td * this.Region.Tiles[i].Index.X + this.mapPositionX + this.mapOffSetX;
             let ty = td * this.Region.Tiles[i].Index.Y + this.mapPositionY + this.mapOffSetY;

             canvasContext.fillStyle = this.Region.Tiles[i].Data.color;
             canvasContext.fillRect(tx, ty, td, td);
         }
        }

        addTile(pixelX, pixelY) {
            let blockX = Math.floor(pixelX / this.scaleFactor);
            let blockY = Math.floor(pixelY / this.scaleFactor);

            this.Region.Tiles.push({
                Index: { X: blockX, Y: blockY },
                Data: { color: "#005500", type: "block" }
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

        Scroll() {
            this.scroll = !this.scroll;
        }

        mouseDown(event) {
            let canvas = <HTMLCanvasElement>document.getElementById('theCanvas');

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


        constructor() {
            this.getScaleFactor();

            this.addTile(0, 50);
            this.RenderCanvas();

            window.addEventListener("resize", this.getScaleFactor);
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
