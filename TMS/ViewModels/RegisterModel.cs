using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.ViewModels
{
    public class RegisterModel
    {
        [Display(Name = "Введите Email")]
        [Required(ErrorMessage = "Не указан Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Введите ShortName")]
        public string ShortName { get; set; }

        [Display(Name = "Введите ФИО")]
        [Required(ErrorMessage = "Не указаны ФИО")]
        public string FullName { get; set; }

        [Display(Name = "Введите пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
