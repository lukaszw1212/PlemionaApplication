using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniProjekt.Enumarable;

namespace MiniProjekt
{
    public class Catapult:Entity
    {
        private int level = 1;
        public override int Level
        {
            get => level;
            set
            {
                if (value < 5)
                {
                    level = value;
                    UpdateProperties();
                }
            }
        }
        private void UpdateProperties()
        {
            MaxHP = (int)(MaxHP * 1.15);
            CurrentHP = MaxHP;
            AttackSpeed = (int)(AttackSpeed * 1.3);
            Damage = (int)(Damage * 1.2);
            PhysicalResistance = (int)(PhysicalResistance * 1.25);
            RangeResistance = (int)(RangeResistance * 1.25);
        }
    }
}
