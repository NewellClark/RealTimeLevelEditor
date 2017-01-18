using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class TypeDbEntry
    {
        public int Id { get; set; }
        public string PropertiesJSON { get; set; } //the list of property names and , in json format
        public string EditorModel { get; set; } //.png file for displaying the type in the editor
        public string InGameModel { get; set; } // the 2d or 3d model for representing the tile in-game
        public string LevelId { get; set; }
    }
}
