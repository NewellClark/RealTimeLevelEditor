namespace WebApi.Controllers {

    export class HomeController {

        public numTiles = 20;
        public scaleFactor = 1;
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

            //canvas.width = window.innerWidth;
            //canvas.height = window.innerHeight - 150;

            

            //let td = null;
            //if (canvas.height < canvas.width) { td = canvas.height / this.numTiles }
            //else {td = canvas.width / this.numTiles;}
            this.getScaleFactor();
            let td = this.scaleFactor;

//let td = canvas.width / 20;//td for tile dimension

         canvasContext.fillRect(td, td, td, td);


         for (let i = 0; i < this.Region.Tiles.length; i++) {
             let tx = this.Region.Tiles[i].Index.X;
             let ty = this.Region.Tiles[i].Index.Y;

             canvasContext.fillStyle = this.Region.Tiles[i].Data.color;
             canvasContext.fillRect(td * tx, td * ty, td, td);
         }
        }

        addBlock(pixelX, pixelY) {
            let blockX = Math.floor(pixelX / this.scaleFactor);
            let blockY = Math.floor(pixelY / this.scaleFactor);

            this.Region.Tiles.push({
                Index: { X: blockX, Y: blockY },
                Data: { color: "#005500", type: "block" }
            });
        }

        mouseDown(event) {
            let canvas = <HTMLCanvasElement>document.getElementById('theCanvas');
            

            this.addBlock(event.x-canvas.offsetLeft, event.y-canvas.offsetTop);
            this.RenderCanvas();
        }

        getScaleFactor() {

            let td = 1;
            let canvas = <HTMLCanvasElement>document.getElementById('theCanvas');
            canvas.width = window.innerWidth;
            canvas.height = window.innerHeight - 150;

            if (canvas.height < canvas.width) { td = canvas.height / this.numTiles }
            else { td = canvas.width / this.numTiles; }
            this.scaleFactor = td;

        }

        constructor() {
            this.getScaleFactor();

            this.addBlock(0, 50);
            this.RenderCanvas();
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
