using DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserNickName")]
        public string UserNickName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "{0} alanına en az {2} karakter giriniz", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

       User User { get; set; }
    }
}
