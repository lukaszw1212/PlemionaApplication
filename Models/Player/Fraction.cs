using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjekt
{
    public class Fraction
    {
        public int Id { get; set; }
        [Required]
        public int Name { get; set; }
        public int? GuildMasterId { get; set; }

        [ForeignKey("GuildMasterId")]
        public Player GuildMaster { get; set; }
        public List<Player> Players { get; set; }

    }
}
