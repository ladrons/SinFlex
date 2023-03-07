using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project.DTO.Internal
{
    public class TicketDTO
    {
        public string ID { get; set; }

        [Display(Name = "Bilet Adı")]
        [Required(ErrorMessage = "{0} boş bırakılamaz!")]
        [MaxLength(12, ErrorMessage = "{0} Maksimum {1} karakter olabilir.")]
        public string Name { get; set; }

        [Display(Name = "Bilet Türü")]
        [Required(ErrorMessage = "{0} boş bırakılamaz!")]
        [MaxLength(10, ErrorMessage = "{0} Maksimum {1} karakter olabilir.")]
        public string Type { get; set; }

        [Display(Name = "Bilet Fiyatı")]
        [Required(ErrorMessage = "{0} boş bırakılamaz!")]
        [Range(1.00D,299.99D,ErrorMessage = "{0} Minimum {1} ₺, Maksimum {2} ₺ Olabilir.")]
        public decimal? Price { get; set; }
    }
}
