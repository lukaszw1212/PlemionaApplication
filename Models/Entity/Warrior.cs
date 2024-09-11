using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjekt
{
    public class Warrior: Entity
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
            MaxHP = (int)(MaxHP * 1.3);
            CurrentHP = MaxHP;
            AttackSpeed = AttackSpeed * 1.15;
            Damage = (int)(Damage * 1.3);
            PhysicalResistance = (PhysicalResistance * 1.25);
            RangeResistance = (RangeResistance * 1.25);
        }

    }
}
