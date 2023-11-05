using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cal.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [Display(Name = "ID Категории")]
        public int CategoryId { get; set; }

        [Display(Name = "Цвет категории")]
        public string? CategoryColor { get; set; }

        [Display(Name = "Категория")]
        public string CategoryName { get; set; }
    }
}
