using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MiniProjekt
{
    public class Expedition
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Level { get; set; }
        public int ExperienceGained { get; set; }
        public DefensiveWalls? DefensiveWalls { get; set;}
        public List<Entity>? Entities { get; set; }
        public List<Resource>? Resources { get; set; }

    }
}
