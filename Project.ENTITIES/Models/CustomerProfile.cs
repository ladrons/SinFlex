using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class CustomerProfile : BaseEntity
    {
        public string? Firstname { get; set; } //İsim
        public string? Lastname { get; set; } //Soyisim
        public string? Gender { get; set; } //Cinsiyet
        public string? PhoneNumber { get; set; } //Telefon Numarası

        public string? WorkingStatus { get; set; } //Çalışma Durumu
        public string? EducationalStatus { get; set; } //Eğitim Seviyesi
        public string? MaritalStatus { get; set; } //Evlilik Durumu

        //Relational Properties

        public virtual Customer Customer { get; set; }
    }
}
