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
            PhysicalResistance = (int)(PhysicalResistance * 1.25);
            RangeResistance = (int)(RangeResistance * 1.25);
        }
    }
}
