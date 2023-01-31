using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project.DTO.Internal
{
    public class SaloonDTO
    {
        public string? ID { get; set; }

        [Display(Name = "Salon Adı")]
        [Required(ErrorMessage = "{0} boş bırakılamaz!")]
        [MinLength(5, ErrorMessage = "{0} Minimum {1} karakter olmalıdır.")]
        public string? Name { get; set; }

        [Display(Name = "Kapasite")]
        [Required(ErrorMessage = "{0} boş bırakılamaz!")]
        public ushort? Capacity { get; set; }
    }
}
