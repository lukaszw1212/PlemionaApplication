using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MiniProjekt.Enumerable;

namespace MiniProjekt
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public virtual int Level { get; set; }
        [Required]

        public string Name { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public double AttackSpeed { get; set; }
        public Damage DamageType { get; set; }
        public int Damage { get; set; }
        public int PhysicalResistance { get; set; }
        public int RangeResistance { get; set; }
        public int VillageId { get; set; }

        [ForeignKey("VillageId")]
        public Village Village { get; set; }
        protected Entity(int level, string name, int currentHp, int maxHp, double attackSpeed, Damage damageType, int damage, int physicalResistance, int rangeResistance)
        {
            Level = level;
            Name = name;
            CurrentHP = currentHp;
            MaxHP = maxHp;
            AttackSpeed = attackSpeed;
            DamageType = damageType;
            Damage = damage;
            PhysicalResistance = physicalResistance;
            RangeResistance = rangeResistance;
        }
    }
  
}
