using System.ComponentModel.DataAnnotations;

namespace Cal.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Электронная почта")]
        [Required(ErrorMessage = "Необходимо ввести электронную почту.")]
        public string Email { get; set; }
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Введите пароль.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set;}
    }
}
