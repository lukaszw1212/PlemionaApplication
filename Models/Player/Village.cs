using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjekt
{
    public class Village
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int PlayerId { get; set; }

        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
        public List<Building> Buildings = new List<Building>();
        public List<Entity> Entities = new List<Entity>();
        

    }
}
