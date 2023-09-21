using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cal.Models
{
    [Table("Events")]
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public AppUser? AppUser { get; set; }
        [ForeignKey("UserId")]
        public string? UserId { get; set; }
        public DateTime Date { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
