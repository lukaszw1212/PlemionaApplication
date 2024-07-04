﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using MiniProjekt.Enumarable;

namespace MiniProjekt
{
    public abstract class Entity
    {
        public virtual int Level { get; set; }

        public string Name { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public double AttackSpeed { get; set; }
        public Damage DamageType { get; set; }
        public int Damage { get; set; }
        public int PhysicalResistance { get; set; }
        public int RangeResistance { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} ;" +
                $"Level: {Level} ;"+
                $"MaxHP: {MaxHP} ;" +
                $"AttackSpeed: {AttackSpeed} ;" +
                $"DamageType: {DamageType} ;" +
                $"Damage: {Damage} ;" +
                $"PhysicalResistance: {PhysicalResistance} ;" +
                $"RangeResistance: {AttackSpeed} ;";
        }
    }
  
}
