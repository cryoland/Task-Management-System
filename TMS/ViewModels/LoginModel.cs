using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.ViewModels
{
    public class LoginModel
    {
        [Display(Name = "Введите Email")]
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Display(Name = "Введите пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
