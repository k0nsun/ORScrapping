namespace EasyOR.MIgration.DTOOld
{
    public class Quete
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public int IDTypeGain { get; set; }
        public int IDNomRecompense { get; set; }
        public int IDTypeRecompense { get; set; }
        public int Valeur { get; set; }
        public int Duree { get; set; }
        public bool Soldat { get; set; }
        public bool Flotte { get; set; }
        public bool Exploration { get; set; }
        public bool Defense { get; set; }
        public string Commentaire { get; set; }
        public string RecompenseDetail { get; set; }
        public int Visible { get; set; }
    }
}
