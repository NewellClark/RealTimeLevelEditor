using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class TypeProperty
    {
        public int Id { get; set; }
        public string LevelId { get; set; }
        public string Property { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
