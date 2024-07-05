using MiniProjekt.Enumerable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniProjekt
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ResourceType Type { get; set; }
        public int Amount {  get; set; }
        public int PlayerId { get; set; }

        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
        protected Resource(string name, int amount)
        {
            Name = name;
            Amount = amount;
            if (name == "Gold")
                Type = ResourceType.Gold;
            else if (name == "Iron")
                Type = ResourceType.Iron;
            else if (name == "Stone")
                Type = ResourceType.Stone;
            else if (name == "Wheat")
                Type = ResourceType.Wheat;
            else if (name == "Wood")
                Type = ResourceType.Wood;
        }

    }
}
