using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjekt
{
    public class Player
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Level { get; set; }
        public int CurrentExperience { get; set; }
        public List<Resource> Resources { get; set; }
        public int? FractionId { get; set; }

        [ForeignKey("FractionId")]
        public Fraction? Fraction { get; set; }
        public List<Village> Villages { get; set; }
    }
}
