using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using MiniProjekt.Enumerable;
using PlemionaApplication.Entities;

namespace MiniProjekt
{
    public class Archer: Entity
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
            MaxHP = (int)(MaxHP * 1.25);
            CurrentHP = MaxHP;
            AttackSpeed = Math.Round(AttackSpeed * 1.15,2);
            Damage = (int)(Damage * 1.3);
            PhysicalResistance = (int)(PhysicalResistance * 1.5);
            RangeResistance = (int)(RangeResistance * 1.5);
        }

    }
}
