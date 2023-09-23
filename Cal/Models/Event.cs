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
        [Display(Name = "Время")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Display(Name="Название")]
        public string? Name { get; set; }
        [Display(Name = "Описание")]
        public string? Description { get; set; }
    }
}
