using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MiniProjekt.Enumerable;

namespace MiniProjekt
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public int Level { get; set; }
        [Required]

        public string Name { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public double AttackSpeed { get; set; }
        public Damage DamageType { get; set; }
        public int Damage { get; set; }
        public double PhysicalResistance { get; set; }
        public double RangeResistance { get; set; }
        public int? VillageId { get; set; }
        public int? ExpeditionId { get; set; }

        [ForeignKey("VillageId")]
        public Village? Village { get; set; }
        
        [ForeignKey("ExpeditionId")]
        public Expedition? Expedition { get; set; }
    }
  
}
