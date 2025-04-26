using System.ComponentModel.DataAnnotations;

namespace OrnekProje.Models.ViewModels
{
    public class Login_VM
    {
        [Required(ErrorMessage = "Kullanıcı adı veya e-posta zorunludur."), Display(Name = "Kullanıcı Adı")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullanıcı adı 3-50 karakter arasında olmalıdır.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur."), DataType(DataType.Password ), Display(Name = "Şifre") ]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
