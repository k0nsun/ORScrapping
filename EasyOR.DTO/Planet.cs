using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyOR.DTO
{
    public class Planet
    {
        public int PlanetId { get; set; }
        public string Name { get; set; }
        public short Galaxy { get; set; }
        public short System { get; set; }
        public short Position { get; set; }
        public int PlayerId { get; set; }
        public DateTime DateCreation { get; set; }
        Player Player { get; set; }  
    }
}
