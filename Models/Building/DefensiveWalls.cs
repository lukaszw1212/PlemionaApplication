using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjekt
{
    public class DefensiveWalls:Building
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
        public int DefensiveValue { get; set; }

        public int MaxDefensiveValue { get; set; }
        public int MaxBuildingLevel { get; set; }

        private void UpdateProperties()
        {
            MaxDefensiveValue = 10 + (Level - 1) * 10;
        }
    }
}
