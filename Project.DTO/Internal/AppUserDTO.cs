using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DTO.Internal
{
    //Bu sınıfı Annotations için kullanacağım. ID özelliği olmasına gerek yok DB'ye yönlendirilirken gerçek nesneye dönüştürülecek.
    public class AppUserDTO
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} boş bırakılamaz!")]
        [MinLength(5, ErrorMessage = "{0} Minimum {1} karakter olmalıdır.")]
        [MaxLength(18, ErrorMessage = "{0} Maksimum {1} karakter olmalıdır.")]
        public string? Username { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "{0} boş bırakılamaz!")]
        [MinLength(6, ErrorMessage = "{0} Minimum {1} karakter olmalıdır.")]
        [MaxLength(15, ErrorMessage = "{0} Maksimum {1} karakter olmalıdır.")]
        public string? Password { get; set; }

        [Display(Name = "İsim")]
        public string? Firstname { get; set; }

        [Display(Name = "Soyisim")]
        public string? Lastname { get; set; }
    }
}