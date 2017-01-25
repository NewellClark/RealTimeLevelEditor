namespace WebApi.Controllers {


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

    export class ProjectDTO {
        public projectId;
        public projectName;
    }




    export class LevelInfoDTO {
        public levelId: string;
        public name: string;
    }



}