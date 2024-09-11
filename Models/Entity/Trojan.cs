using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjekt
{
    public class Trojan: Entity
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
            MaxHP = (int)(MaxHP * 1.35);
            CurrentHP = MaxHP;
            AttackSpeed = Math.Round(AttackSpeed * 1.2,2);
            Damage = (int)(Damage * 1.3);
            PhysicalResistance = (PhysicalResistance * 1.2);
            RangeResistance = (RangeResistance * 1.2);
        }
    }
}
