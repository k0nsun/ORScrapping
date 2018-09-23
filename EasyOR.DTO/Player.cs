using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyOR.DTO
{ 
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public bool IsAFK { get; set; }
        public bool IsVacation { get; set; }
        public int InternalIdOR { get; set; }
        public IList<Planet> Planets { get; set; }
    }
}
