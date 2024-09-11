using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjekt
{
    public class HorseStable:Building
    {
        private int level = 1;
        public override int Level
        {
            get => level;
            set
            {
                if (value <= MaxBuildingLevel)
                {
                    level = value;
                    UpdateProperties();
                }
            }
        }
        public int CurrentHorses { get; set; }

        public int MaxHorses { get; set; }
        public int MaxBuildingLevel { get; set; }

        private void UpdateProperties()
        {
            MaxHorses++;
        }

    }
}
