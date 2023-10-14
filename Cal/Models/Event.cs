using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cal.Models
{
    [Table("Events")]
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public Category Category { get; set; }
        public AppUser? AppUser { get; set; }
        [ForeignKey("UserId")]
        public string? UserId { get; set; }
        [Display(Name = "Время")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Display(Name="Название")]
        [Required(ErrorMessage = "Это поле не может быть пустым.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Поле не может быть пустым.")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Это поле не может быть пустым.")]
        [StringLength(3000, MinimumLength = 1, ErrorMessage = "Поле не может быть пустым.")]
        public string Description { get; set; }
    }
}
