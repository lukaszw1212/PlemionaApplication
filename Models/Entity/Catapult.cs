using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniProjekt;

namespace MiniProjekt
{
    public class Catapult : Entity
    {
        public void UpgradeLevel()
        {
            if (Level < 5) // Upewnij się, że maksymalny poziom to 5
            {
                Level++;
                UpdateProperties();
            }
        }
        private void UpdateProperties()
        {
            MaxHP = (int)(MaxHP * 1.15);
            CurrentHP = MaxHP;
            AttackSpeed = Math.Round(AttackSpeed * 1.2,2);
            Damage = (int)(Damage * 1.2);
            PhysicalResistance = (int)(PhysicalResistance * 1.25);
            RangeResistance = (int)(RangeResistance * 1.25);
        }
    }
}
