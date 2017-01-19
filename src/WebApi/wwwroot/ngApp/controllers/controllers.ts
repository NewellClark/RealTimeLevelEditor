namespace WebApi.Controllers {

    export class HomeController {


        public anchorX = 0;
        public anchorY = 0;
        public anchoring = false;
        public mapOffSetX = 0;
        public mapOffSetY = 0;

        public mapPositionX = 0;
        public mapPositionY = 0;

        public loadX = 0;
        public loadY = 0;

        public attributeList = [];

        public tileTypes = [];

        public levelId = "7932f7c5-b26d-4592-06e7-08d43e66e5c9";

        public chunkQueue = [{ x: 1000, y: 1000, loaded: true, inView: false }];
        public tileLimit = 50000;
        
        public selecting = false;
        public selectAnchoring = false;
        public selectX0 = 0;
        public selectY0 = 0;
        public selectX1 = 0;
        public selectY1 = 0;

        public propertyBuffer = [];

        public onChunk = [{ x: 0, y: 0 }, { x: 0, y: 0 }, { x: 0, y: 0 }, { x: 0, y: 0 }];
        public pOnChunk = [{ x: 0, y: 0 }, { x: 0, y: 0 }, { x: 0, y: 0 }, { x: 0, y: 0 }];
        

        public mpx = 0;
        public mpy = 0;

        public pmpx = 0;
        public pmpy = 0;

        public chunkWidth = 100;
        public chunkHeight = 100;

        public BMPIndex = 0;

        public addToSelect;

        public tileImagesData = [];

        public chunks = [];// [{ x: 0, y: 0, inRange: true, toBeDeleted: false, deleteTimer: -1 }];

        public deleteWaitTime = 500;

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
            ]
        };

        public Region20: Array<DTOChunk> = [];

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

        addProject() {
            let data = "Project1";
            this.$http.post(`api/projects/22686b5a-7d4a-4e5c-8c12-23bf2f762c3e/${data}`, data);
        }

        addMember() {
            //let data = { name: "Stephen.Walther@CoderCamps.com" };
            let projectId = "09c40875-22cd-42de-8976-863fc6dddadd";
            let newUserId = "40727362-7186-412d-8361-5a0f8d894617";
            this.$http.post(`api/members/${projectId}/${newUserId}`,null);
        }

        addLevel() {
            
        }

    

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

            this.RenderCanvas20();
            return;

          /*  let canvas = <HTMLCanvasElement>document.getElementById('theCanvas');
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

          this.drawRadar();
          */
        }

        RenderCanvas20() {

            let canvas = <HTMLCanvasElement>document.getElementById('theCanvas');
            let canvasContext: CanvasRenderingContext2D = canvas.getContext('2d');

            this.getScaleFactor();

            let td = this.scaleFactor;

            for (let n = 0; n < this.Region20.length; n++) {

                for (let i = 0; i < this.Region20[n].data.tiles.length; i++) {
                    let tx = td * this.Region20[n].data.tiles[i].index.x + this.mapPositionX + this.mapOffSetX;
                    let ty = td * this.Region20[n].data.tiles[i].index.y + this.mapPositionY + this.mapOffSetY;

                    //canvasContext.fillStyle = this.Region20[n].data.tiles[i].data.;
                    if (this.Region20[n].data.tiles[i].data == null) {
                       // canvasContext.fillRect(tx, ty, td, td);
                    }
                    else {
                        let j = this.Region20[n].data.tiles[i].data;
                        canvasContext.drawImage(this.tileImagesData[j], tx, ty, td, td);
                    }
                }
            }

            if (this.selectAnchoring == true) {
                canvasContext.setLineDash([5, 15]);
                canvasContext.rect(this.selectX0, this.selectY0,
                    this.selectX1 - this.selectX0, this.selectY1 - this.selectY0);
                canvasContext.stroke();
            }

            if (this.hasSelection == true)
                this.drawSelection();

            this.drawRadar();

        }

        public addRandom() {
            for (let i = 0; i < 10000; i++) {
                let x = Math.floor(Math.random() * 500);
                let y = Math.floor(Math.random() * 500);

                this.Region.Tiles.push({
                    Index: { X: x, Y: y },
                    Data: { color: this.colorToAdd, type: "block", imageInfo: Math.floor(Math.random()*9+1) }
                });
                
            }
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


        //2.0 versions of add and remove tile
        //////////////////////////////////////
        addTile20(pixelX, pixelY) {

            let select = <HTMLSelectElement>document.getElementById("repeatSelect");

            let imageInfo = null;
            if (select.selectedIndex > 0) {
                imageInfo = select.selectedIndex;//this.tileImagesFiles[select.selectedIndex];
                let x = 0;
            }

            let bx = Math.floor(pixelX / this.scaleFactor);
            let by = Math.floor(pixelY / this.scaleFactor);

            this.$http.put(`api/levels/${this.levelId}/tiles`, [{ data: imageInfo, index: { x: bx, y: by } }]).then(() => {
            });

            for (let i = 0; i < this.Region20.length; i++) {
                let left = this.Region20[i].data.region.left;
                let top = this.Region20[i].data.region.top;
                let right = left + this.Region20[i].data.region.width;
                let down = top + this.Region20[i].data.region.height;

                if (bx >= left && by >= top && bx <= right && by <= down)

                    //let data = ;

                    this.Region20[i].data.tiles.push({ data: imageInfo, index: { x: bx, y: by } });
 

            }

            this.RenderCanvas20();
        }

        removeTile20(pixelX, pixelY) {

            
            let bx = Math.floor(pixelX / this.scaleFactor);
            let by = Math.floor(pixelY / this.scaleFactor);

            this.$http.put(`api/levels/${this.levelId}/tiles`, [{ data: "3", index: { x: bx, y: by } }]).then(() => {
            });

            for (let i = 0; i < this.Region20.length; i++) {
                let left = this.Region20[i].data.region.left;
                let top = this.Region20[i].data.region.top;
                let right = left + this.Region20[i].data.region.width;
                let down = top + this.Region20[i].data.region.height;

                if (bx >= left && by >= top && bx <= right && by <= down) {
                    for (let j = 0; j < this.Region20[i].data.tiles.length; j++) {
                        if (this.Region20[i].data.tiles[j].index.x == bx &&
                            this.Region20[i].data.tiles[j].index.y == by)
                        this.Region20[i].data.tiles.splice(j, 1);

                    }
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
                    this.addTile20(event.x - canvas.offsetLeft - this.mapPositionX, event.y - canvas.offsetTop - this.mapPositionY);
                                                
                    this.RenderCanvas20();
                }
                if (event.button == 2) {
                    this.removeTile20(event.x - canvas.offsetLeft - this.mapPositionX, event.y - canvas.offsetTop - this.mapPositionY);
                    this.RenderCanvas20();
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

            if (this.scroll == true) this.findTilePosition();
            this.RenderCanvas20();
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

            this.loadProperty(this.tileImages[this.BMPIndex].name);

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

        public createLevel(){
            
            let data = {Name: "NewLevel"};
            this.$http.post("api/levels", data).then((returned) => {
                let rData = returned.data;

            });

        }

        public saveRegion() {

            let data = [];
            for (let i = 0; i < this.Region.Tiles.length; i++){
                let x = this.Region.Tiles[i].Index.X;
                let y = this.Region.Tiles[i].Index.Y;
                let t = this.Region.Tiles[i].Data.imageInfo;

                data.push({ index: { x: x, y: y }, data: t });

            }


            this.$http.put(`api/levels/${this.levelId}/tiles`, data)
                .then((returned) => {
                    let rData = returned.data;
                });
        }

        public nearestHundred(n) {
            return 100 * Math.floor(n / 100);
        }

       
       
        public pointInSquare(x, y, sqx, sqy, sqw, sqh) {
            if (x > sqx && x < sqx + sqw && y > sqy && y < sqy + sqw)
                return true;
            return false;
        }

        public loadRegion(x, y, w, h) {

            
           
            let requestBody = {
                left: x * this.chunkWidth,
                top: y * this.chunkHeight,
                width: w * this.chunkWidth,
                height: h * this.chunkHeight
            };

            this.$http.post(`api/levels/${this.levelId}/region`, requestBody)
                .then((responseBody) => {
                    let chunks = <DTOChunk[]>responseBody.data;

                    //this.Region.Tiles = [];

                    for (let i = 0; i < chunks.length; i++) {
                        for (let j = 0; j < chunks[i].data.tiles.length; j++) {

                            let indexX = chunks[i].data.tiles[j].index.x;
                            let indexY = chunks[i].data.tiles[j].index.y;
                            let imageIndex = parseInt(chunks[i].data.tiles[j].data);

                            if (imageIndex > 0) {
                                this.removeTile(indexX, indexY);
                                this.Region.Tiles.push({
                                    Index: { X: indexX, Y: indexY },
                                    Data: { color: this.colorToAdd, type: "block", imageInfo: imageIndex }
                                });
                                if (this.Region.Tiles.length > this.tileLimit) return;
                            }
                        }
                    }

                    //  this.chunks.push({
                    //    x: x,
                    //    y: y,
                    //    inRange: true,
                    //    toBeDeleted: false,
                    //    deleteTimer: -1
                    //});

                });
        }

        public loadLevel() {



            let requestBody = { left: 0, top: 0, width: 500, height: 500 };

            this.$http.post(`api/levels/${this.levelId}/region`, requestBody)
                .then((responseBody) => {
                    let chunks = <DTOChunk[]>responseBody.data;

                    this.Region.Tiles = [];

                    for (let i = 0; i < chunks.length; i++) {
                        for (let j = 0; j < chunks[i].data.tiles.length; j++) {

                            let indexX = chunks[i].data.tiles[j].index.x;
                            let indexY = chunks[i].data.tiles[j].index.y;
                            let imageIndex = parseInt(chunks[i].data.tiles[j].data);

                            if (imageIndex > 0) {
                                this.Region.Tiles.push({
                                    Index: { X: indexX, Y: indexY },
                                    Data: { color: this.colorToAdd, type: "block", imageInfo: imageIndex }
                                });
                            }
                        }
                    }


                });
        }

        //////////////////////////////
        //Chunk 2.0 Code

        public loadRegionObjects(x, y, w, h) {



            let requestBody = {
                left: x * this.chunkWidth,
                top: y * this.chunkHeight,
                width: w * this.chunkWidth,
                height: h * this.chunkHeight
            };

            this.$http.post(`api/levels/${this.levelId}/region`, requestBody)
                .then((responseBody) => {
                    let chunks = <DTOChunk[]>responseBody.data;
                    for (let i = 0; i < chunks.length; i++)
                     this.Region20.push(chunks[i]);
                });
        }

        //////////////////////////////
        //Type code

        public testType() {
            this.$http.get(`api/types/${this.levelId}`).then((res) => {
                let types = <DTOType[]>res.data;
                console.log(types);
            });
        }

        //public tileImages = [
        //    { id: 0, name: "-------------" },
        //    { id: 1, name: "tilemario.png" },
        //];

        //public tileImagesFiles = [
        //    "-------------",
        //    "tilemario.png",
        //    "tilegoomba.png",
        //];

        public loadTypes() {
            this.$http.get(`api/types/${this.levelId}`).then((res) => {
                let types = <DTOType[]>res.data;

                console.log(types);
                this.tileTypes = types;

                this.tileImages = []; this.tileImagesFiles = [];

                this.tileImages.push({ id: 0, name: "---------" });
                this.tileImagesFiles.push("--------");
                for (let i = 0; i < types.length; i++) {
                    this.tileImages.push({ id: i + 1, name: types[i].name });
                    this.tileImagesFiles.push(types[i].tileModel);
                }
                this.loadImageTiles();
               // this.loadProperties();

            });
        }

        //public loadProperties() {

        //    for (let i = 0; i < this.tileTypes.length; i++) {
        //        this.attributeList.push([]);
        //    }

        //    for (let i = 0; i < this.tileTypes.length; i++) {
        //        let name = this.tileTypes[i].name;
        //        this.loadProperty(name, i);
        //    }

        //}

        loadProperty(name) {
            this.$http.get(`api/properties/${this.levelId}/${name}`).then((res) => {
                this.propertyBuffer = <Property[]>res.data;
                console.log(res.data);
            });
        }

        ////////////////////////////
        //Chunk loading/unloading code
            public findTilePosition() {

                this.pmpx = this.mpx;
                this.pmpy = this.mpy;

                this.mpx = -Math.floor(this.mapPositionX / this.scaleFactor);
                this.mpy = -Math.floor(this.mapPositionY / this.scaleFactor);



           }

            public whichChunkX(i) {

                let plusx = 0;

                if (i == 1 || i == 3) plusx = this.chunkWidth-1;
                //get distance in number of tiles rounded to nearest tile
                return Math.floor((this.mpx + plusx) / this.chunkWidth);

            }

            public whichChunkY(i) {

                let plusy = 0;
                if (i == 2 || i == 3) plusy = this.chunkHeight-1;
                //get distance in number of tiles rounded to nearest tile
                return Math.floor((this.mpy + plusy) / this.chunkHeight);

            }

            doOnChunks() {

                for (let i = 0; i < 4; i++) {
                    this.pOnChunk[i].x = this.onChunk[i].x;
                    this.pOnChunk[i].y = this.onChunk[i].y;
                }

                for (let i = 0; i < 4; i++) {
                    this.onChunk[i].x = this.whichChunkX(i);
                    this.onChunk[i].y = this.whichChunkY(i);
                }

            }

            doHasCrossedChunks() {
                for (let i = 0; i < 4; i++) {
                    if (this.pOnChunk[i].x != this.onChunk[i].x ||
                        this.pOnChunk[i].y != this.onChunk[i].y)
                        console.log("Point: " + i + " crossed chunks."); 
                }
            }

            updateChunkStatus() {
                this.updateChunkAdd();
                this.updateChunkRemove();
                this.handleChunkRemoval();
            }

            updateChunkAdd() {
                //add the chunk we're on to the chunks stack
                //but only if it's not already there

                for (let i = 0; i < 4; i++) {
                    let canAdd = true;

                    for (let j = 0; j < this.chunks.length; j++) {

                        if (this.onChunk[i].x == this.chunks[j].x &&
                            this.onChunk[i].y == this.chunks[j].y)
                        { canAdd = false; }
                    }
                    if (canAdd) {
                        this.loadChunkXY(this.onChunk[i].x, this.onChunk[i].y);
                        this.chunks.push({
                            x: this.onChunk[i].x,
                            y: this.onChunk[i].y,
                            inRange: true,
                            toBeDeleted: false,
                            deleteTimer: -1
                        });

                    }
                }
                    

                
            }

            loadChunkXY(x, y) {

                this.loadRegionObjects(x, y, 1, 1);
               // this.loadRegion(x, y,
               //                 1, 1);

            }

            deleteChunkXY(x, y) {

                let left = x * this.chunkWidth;
                let top = y * this.chunkHeight;
                let right = x + this.chunkWidth - 1;
                let down = y + this.chunkHeight - 1;


                for (let i = this.Region.Tiles.length-1; i > -1; i--) {
                    let tx = this.Region.Tiles[i].Index.X;
                    let ty = this.Region.Tiles[i].Index.Y;

                    if (tx >= left && tx <= right && ty >= top && ty <= down)
                        this.Region.Tiles.splice(i, 1);
                    
                }
                
            }

              updateChunkRemove() {
                //test if the chunks are visible
                //if they are, set them to visible and reset removal clock
                //if not, schedule them for eventual deletion
                for (let i = 0; i < this.chunks.length; i++) {
                    let inView = false;
                    for (let j = 0; j < 4; j++) {
                        if (this.chunks[i].x == this.onChunk[j].x &&
                            this.chunks[i].y == this.onChunk[j].y)
                        { inView = true; }
                    }
                    if (inView) {
                        this.chunks[i].deleteTimer = -1;
                        this.chunks[i].inRange = true;
                        this.chunks[i].toBeDeleted = false;
                    }
                    else if (this.chunks[i].toBeDeleted == false)
                    {
                        this.chunks[i].deleteTimer = this.deleteWaitTime;
                        this.chunks[i].inRange = false;
                        this.chunks[i].toBeDeleted = true;
                    }
                }
            }

            handleChunkRemoval() {

                let deleteIndices = [];
    
                for (let i = 0; i < this.chunks.length; i++) {
                    if (this.chunks[i].toBeDeleted)
                    {
                        this.chunks[i].deleteTimer--;
                    }
                    if (this.chunks[i].deleteTimer == 0) {
                        deleteIndices.push(i);
                    }

                }

                if (deleteIndices != []) {
                    for (let i = deleteIndices.length - 1; i > -1; i--) {
                        this.deleteChunkXY20(this.chunks[deleteIndices[i]].x, this.chunks[deleteIndices[i]].y);
                        this.chunks.splice(deleteIndices[i], 1);                        
                    }
                }
            }

            public deleteChunkXY20(x, y) {
                for (let i = 0; i < this.Region20.length; i++) {
                    if (x * this.chunkWidth == this.Region20[i].data.region.left &&
                        y * this.chunkHeight == this.Region20[i].data.region.top)
                        this.Region20.splice(i, 1);
                }
            }

            public drawRadar() {

                this.doOnChunks();
                this.doHasCrossedChunks();
                this.updateChunkStatus();

                let canvas = <HTMLCanvasElement>document.getElementById('theCanvas');
                let canvasContext: CanvasRenderingContext2D = canvas.getContext('2d');

                let rBound = document.getElementById('sideMenu').clientLeft;
                let dBound = window.innerHeight;

                let xOffset = canvas.width - 320;
                let yOffset = canvas.height - 170;
                canvasContext.fillStyle = "#000000";
                canvasContext.fillRect(xOffset, yOffset, 150, 150);

                canvasContext.strokeStyle = "#00FF00";
                for (let i = 0; i < 11; i++) {
                    canvasContext.beginPath();
                    canvasContext.moveTo(xOffset + i * 15, yOffset);
                    canvasContext.lineTo(xOffset + i * 15, yOffset + 150);
                    canvasContext.stroke();

                    canvasContext.beginPath();
                    canvasContext.moveTo(xOffset, yOffset + i * 15);
                    canvasContext.lineTo(xOffset + 150, yOffset + i * 15);
                    canvasContext.stroke();
                 }

                for (let i = 0; i < this.chunks.length; i++) {
                    canvasContext.fillStyle = "#FF0000";
                    let chunkX = this.chunks[i].x * 15;
                    let chunkY = this.chunks[i].y * 15;

                    canvasContext.fillRect(xOffset + chunkX, yOffset + chunkY, 15, 15);
                }

                for (let i = 0; i < 4; i++) {
                    canvasContext.fillStyle = "#FF00FF";
                    let chunkX = this.whichChunkX(i) * 15;
                    let chunkY = this.whichChunkY(i) * 15;

                    canvasContext.fillRect(xOffset + chunkX, yOffset + chunkY, 15, 15);
                }

                canvasContext.strokeStyle = "#FFFF00";
                let xPos = this.mpx * 0.15; let yPos = this.mpy * 0.15;
                canvasContext.strokeRect(xOffset + xPos, yOffset + yPos, 15, 15);

                    
            }


        

        constructor(private $http: ng.IHttpService) {
            this.getScaleFactor();
            this.addToSelect = false;

            this.addTile(0, 50);
            //this.loadChunkXY(0, 0);
            this.loadRegionObjects(0, 0, 1, 1);
            //this.RenderCanvas();

            this.loadTypes();

            window.addEventListener("resize", this.getScaleFactor);
            window.addEventListener("keydown", this.keyDown);
            window.addEventListener("keyup", this.keyUp);
        }



    }

    export class DTOChunk {
        public index: DTOIndex;
        public data: DTOData;
    }

    export class DTOData {
        public region: DTORegion;
        public tiles: Array<DTOTile>;
    }

    export class DTORegion {
        public top: number;
        public left: number;
        public width: number;
        public height: number;
    }

    export class DTOTile {
        public index: DTOIndex;
        public data: string;
    }

    export class DTOIndex {
        public x: number;
        public y: number;
    }

    export class DTOType {
        public tileModel: string;
        public inGameModel: string;
        public name: string;
    }

    export class Property {
        public name: string;
        public value: string;
    }

    //    public Region = {
    //    Dimensions: {
    //        Left: 0,
    //        Top: 0,
    //        Width: 20,
    //        Height: 20
    //    },

    //    Tiles:
    //    [
    //        {
    //            Index: { X: 1, Y: 5 },
    //            Data: { color: "#005500", type: "block", imageInfo: null }
    //        },
    //        {
    //            Index: { X: 3, Y: 9 },
    //            Data: { color: "#000099", type: "block", imageInfo: null }
    //        },
    //        {
    //            Index: { X: 10, Y: 18 },
    //            Data: { color: "#ff0000", type: "block", imageInfo: null }
    //        }
    //    ]
    //};

    export class RegionDTO {
        public Dimensions: DimensionDTO;
        public Tiles: TilesDTO;
    }

    export class DimensionDTO {
        public Left: number;
        public Top: number;
        public Width: number;
        public Height: number;
    }

    export class TilesDTO {
        public Index: IndexDTO;
        public Data: string;
    }
    export class IndexDTO {
        public X: number;
        public Y: number;
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
