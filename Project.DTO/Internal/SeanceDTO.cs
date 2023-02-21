using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DTO.Internal
{
    public class SeanceDTO
    {
        [Display(Name ="Seans Başlangıç Tarihi")]
        [Required(ErrorMessage = "{0} boş bırakılamaz!")]
        [DataType(DataType.Date, ErrorMessage = "Lütfen geçerli bir tarih giriniz.")]
        public DateTime StartingDate { get; set; }

        [Display(Name = "Seans Bitiş Tarihi")]
        [Required(ErrorMessage = "{0} boş bırakılamaz!")]
        [DataType(DataType.Date, ErrorMessage = "Lütfen geçerli bir tarih giriniz.")]
        public DateTime EndingDate { get; set; }

        [Required(ErrorMessage = "{0} boş bırakılamaz!")]
        public List<DateTime> SeanceTimes { get; set; }
    }
}
