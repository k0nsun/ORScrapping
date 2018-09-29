using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyOR.Parser.Controleur.ObjectData
{
    public class Player
    {
        public string Name;
        public int UniqueNumberContact;
        public long Point;
        public List<Planete> Planetes;
        public bool QuestPlayer;

        public Player()
        {
            Planetes = new List<Planete>();
        }
    }
}