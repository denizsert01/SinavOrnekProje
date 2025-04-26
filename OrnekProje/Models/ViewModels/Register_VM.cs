using System.ComponentModel.DataAnnotations;

namespace OrnekProje.Models.ViewModels
{
    public class Register_VM
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur."), Display(Name = "Kullanıcı adı")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullanıcı adı 3-50 karakter arasında olmalıdır.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur."), Display(Name = "E-posta")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi.")]
        [StringLength(100, ErrorMessage = "E-posta adresi 100 karakterden uzun olamaz.")]
        public string Email { get; set; }

        [Required, Display(Name = "Ad")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ad 2-50 karakter arasında olmalıdır.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur."), Display(Name = "Soyad")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Soyad 2-50 karakter arasında olmalıdır.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur."), DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\w\d\s]).{6,}$",
    ErrorMessage = "Şifre en az 6 karakter, 1 büyük harf, 1 sayı ve 1 özel karakter içermelidir.")]

        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı zorunludur."),  Display(Name = "Şifre Tekrarı")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword
        {
            get; set;

        }
    }
}
