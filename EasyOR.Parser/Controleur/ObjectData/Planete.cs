using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyOR.Parser.Controleur.ObjectData
{
    public class Planete
    {
        #region membre
        private int m_galaxy;
        public int Galaxy
        {
            get { return this.m_galaxy; }
            set { this.m_galaxy = value; }
        }
        private int m_system;
        public int System
        {
            get { return this.m_system; }
            set { this.m_system = value; }
        }
        private int m_position;
        public int Position
        {
            get { return this.m_position; }
            set { this.m_position = value; }
        }

        public string Name;
        #endregion

        public Planete()
        {

        }

        public Planete(int galaxy, int system, int position)
        {
            this.Galaxy = galaxy;
            this.System = system;
            this.Position = position;

        }
    }
}
