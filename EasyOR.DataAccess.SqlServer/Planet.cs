using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyOR.DataAccess.SqlServer
{
    public class Planet
    {
        public int PlanetId { get; set; }
        public short Galaxy { get; set; }
        public short System { get; set; }
        public short Place { get; set; }
        public DateTime DateCreation { get; set; }
        Player Player { get; set; }  
    }
}
