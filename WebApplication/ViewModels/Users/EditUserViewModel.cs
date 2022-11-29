using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.ViewModels.Users
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        //[Display(Name = "Имя")]
        //public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Display(Name = "Роль")]
        public string UserRole { get; set; }
        public EditUserViewModel()
        {
            UserRole = "user";
        }
    }
}
