using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project.DTO.Internal
{
    public class MovieDTO
    {
        public string? ID { get; set; }

        [Display(Name = "Film Adı")]
        [Required(ErrorMessage = "{0} boş bırakılamaz!")]
        [MinLength(2, ErrorMessage = "{0} Minimum {1} karakter olmalıdır.")]
        public string? Title { get; set; } //Başlık

        [Display(Name = "Konu")]
        [Required(ErrorMessage = "{0} boş bırakılamaz!")]
        [MinLength(35, ErrorMessage = "{0} Minimum {1} karakter olmalıdır.")]
        public string? Content { get; set; } //Film içeriği/konusu

        [Display(Name = "Tür")]
        [Required(ErrorMessage = "{0} boş bırakılamaz!")]
        public string? Genre { get; set; } //Tür

        [Display(Name = "Süre")]
        [Required(ErrorMessage = "{0} boş bırakılamaz!")]
        public string? Duration { get; set; } //Süre

        [DataType(DataType.Date)]
        [Display(Name = "Yayın Tarihi")]
        [Required]
        public DateTime ReleaseDate { get; set; } //Yayın tarihi
    }
}
//[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]