namespace MiniProjekt
{
    public class BattleResult
    {
        public bool PlayerWin { get; set; }
        public int PlayerRemainingHealth { get; set; }
        public int EnemyRemainingHealth { get; set; }
        public int? ExperienceGained {  get; set; }
        public int? GoldGained { get; set; }
        public int? IronGained { get; set; }
        public int? StoneGained { get; set; }
        public int? WoodGained { get; set; }
        public int? WheatGained { get; set; }
    }
}
