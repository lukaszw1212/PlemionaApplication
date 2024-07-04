﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjekt
{
    public class Wood:Resource
    {
        public int WoodNumber { get; set; }

        public Wood(string name, int woodNumber):base(name, woodNumber)
        {
        }

        public override string ToString()
        {
            return " Number of " + base.ToString() + $" needed: {base.Amount}";
        }
    }
}
