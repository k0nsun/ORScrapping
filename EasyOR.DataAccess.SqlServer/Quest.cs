namespace EasyOR.DataAccess.SqlServer
{
    public class Quest
    {
        public int QuestId { get; set; }
        public string Name { get; set; }
        public int ValueProfit { get; set; }
        public int DurationProfil { get; set; }
        public bool HasSoldier { get; set; }
        public bool HasSpaceship { get; set; }
        public bool HasExploration { get; set; }
        public bool HasDefense { get; set; }
        public string Comment { get; set; }
        public int Visible { get; set; }
        public string ProfilOldDatabase { get; set; }
        public RewardType RewardType { get; set; }
        public Profit Profit { get; set; }
        public ProfitType ProfitType { get; set; }
        public Player Player { get; set; }
    }
}
