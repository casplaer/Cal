using System.ComponentModel.DataAnnotations;

namespace Cal.Models
{
    public class Category
    {
        [Display(Name = "Цвет категории")]
        public string CategoryColor { get; set; }

        [Display(Name = "Категория")]
        public string CategoryName { get; set; }

    }
}
