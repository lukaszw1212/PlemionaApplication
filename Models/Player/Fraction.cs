using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlemionaApplication.Entities;

namespace PlemionaApplication.Models
{
    public class Fraction
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? GuildMasterId { get; set; }

        [ForeignKey("GuildMasterId")]
        public Player GuildMaster { get; set; }
        public List<Player> Players { get; set; }

    }
}
