﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjekt
{
    public class Sawmill:Building
    {
        public int GenerateWoodPerTime { get; set; }
        public int MaxWoodPerTime { get; set; }

        public int Time { get; set; }

        public Sawmill(string Name, int Level):base(Name, Level)
        {
            this.GenerateWoodPerTime = 10;
            this.MaxWoodPerTime = 500;
            this.Time = 30;
        }

        public Sawmill() : base()
        {
            this.GenerateWoodPerTime = 0;
        }

        public override string ToString()
        {
            return base.ToString() + $"GenerateWoodPerTime: {GenerateWoodPerTime}\n";
        }
    }
}
