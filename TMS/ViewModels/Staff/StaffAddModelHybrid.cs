using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMS.ViewModels
{
    public class StaffAddModelHybrid
    {
        [Display(Name = "ShortName")]
        [Required(ErrorMessage = "Не указан shortname")]
        public string ShortName { get; set; }

        [Display(Name = "FullName")]
        [Required(ErrorMessage = "Не указаны ФИО")]
        public string FullName { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        public int Role { get; set; }

        [Display(Name = "Select Role")]
        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}