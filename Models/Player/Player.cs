﻿using MiniProjekt.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniProjekt
{
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int CurrentExperience { get; set; }
        public List<Resource> Resources { get; set; }
        public Fraction? Fraction { get; set; }
        public List<Village> Villages { get; set; }
        public event PlayerExpAction OnExpGained;
        public event PlayerAction OnLvlGained;
        public event PlayerAction OnEntitesAdded;
        public event PlayerAction OnUpgrade;
        public event PlayerAction OnBuild;
        public event PlayerAction OnEntityUpgrade;
        public void AddExp(int exp)
        {
            CurrentExperience += exp;
            OnExpGained?.Invoke(this, exp);
            CheckLevel();
        }

        private void CheckLevel()
        {
            int requiredExp = CalculateRequiredExperience(Level);
            while (CurrentExperience >= requiredExp)
            {
                CurrentExperience -= requiredExp;
                Level++;
                OnLvlGained?.Invoke(this);
                requiredExp = CalculateRequiredExperience(Level);
            }
        }

        public int CalculateRequiredExperience(int level)
        {
            return level * 20;
        }
        public bool HasEnoughResources(List<Resource> requiredResources)
        {
            foreach (var required in requiredResources)
            {
                var playerResource = Resources.FirstOrDefault(r => r.Name == required.Name);
                if (playerResource == null || playerResource.Amount < required.Amount)
                {
                    return false;
                }
            }
            return true;
        }
        public void afterBuildingBought(List<Resource> requiredResources)
        {
            foreach (var required in requiredResources)
            {
                var playerResource = Resources.FirstOrDefault(r => r.Name == required.Name);
                if (playerResource != null)
                {
                    playerResource.Amount -= required.Amount; 
                }
            }
            OnBuild?.Invoke(this);
        }
        public void afterEntitesRecruited(List<Resource> requiredResources)
        {
            foreach (var required in requiredResources)
            {
                var playerResource = Resources.FirstOrDefault(r => r.Name == required.Name);
                if (playerResource != null)
                {
                    playerResource.Amount -= required.Amount;
                }
            }
            OnEntitesAdded?.Invoke(this);
        }
        public void afterUpgrade(List<Resource> requiredResources)
        {
            foreach (var required in requiredResources)
            {
                var playerResource = Resources.FirstOrDefault(r => r.Name == required.Name);
                if (playerResource != null)
                {
                    playerResource.Amount -= required.Amount;
                }
            }
            OnUpgrade?.Invoke(this);
        }
        public void afterEntityUpgrade(List<Resource> requiredResources)
        {
            foreach (var required in requiredResources)
            {
                var playerResource = Resources.FirstOrDefault(r => r.Name == required.Name);
                if (playerResource != null)
                {
                    playerResource.Amount -= required.Amount;
                }
            }
            OnEntityUpgrade?.Invoke(this);
        }
        public override string ToString()
        {
            string temp = "", temp2 = "";
            foreach (Resource resource in Resources)
            {
                temp += resource.ToString() + '\n';
            }
            foreach (Village village in Villages)
            {
                temp2 += village.ToString() + '\n';
            }
            return $"Name: {Name}\nLevel: {Level}\nCurrentExperience: {CurrentExperience}\nResource: {temp}\nFraction: {Fraction}\nVillages: {temp2}\n";
        }

    }
}
