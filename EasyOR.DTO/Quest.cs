namespace EasyOR.DTO
{
    public class Quest
    {
        public int QuestId { get; set; }
        public string Name { get; set; }
        public short? ValueProfit { get; set; }
        public short? DurationProfil { get; set; }
        public bool? HasSoldier { get; set; }
        public bool? HasSpaceship { get; set; }
        public bool? HasExploration { get; set; }
        public bool? HasDefense { get; set; }
        public string Comment { get; set; }
        public int Visible { get; set; }
        public string ProfilOldDatabase { get; set; }
        public bool IsCheckName { get; set; }
        public int? RewardTypeId { get; set; }
        public RewardType RewardType { get; set; }
        public int? ProfitId { get; set; }
        public Profit Profit { get; set; }
        public int? ProfitTypeId { get; set; }
        public ProfitType ProfitType { get; set; }
        public int? PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
