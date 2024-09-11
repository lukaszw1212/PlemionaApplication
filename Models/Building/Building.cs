using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjekt
{
    public abstract class Building
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual int Level { get; set; }
        public int? VillageId { get; set; }
        public int? ExpeditionId { get; set; }  

        [ForeignKey("VillageId")]
        public Village? Village { get; set; }
        
        [ForeignKey("ExpeditionId")]
        public Expedition? Expedition { get; set; }  // Dodaj ten wiersz
    }
}
