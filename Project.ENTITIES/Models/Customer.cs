using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Customer : BaseEntity
    {
        public string Username { get; set; } //Kullancı adı
        public string Password { get; set; } //Şifre
        public string EMail { get; set; } //E-Posta adresi
        public DateTime BirthDate { get; set; } //Doğum Tarihi

        public bool AccountConfirm { get; set; } //Hesap onay durumu (False ise hesap onaylanmamıştır)
        public Guid ActivationCode { get; set; } //Aktivasyon kodu

        public CustomerStatus CustomerStatus { get; set; } //Müşteri düzey seviyesi

        public Customer()
        {
            CustomerStatus = CustomerStatus.Normal;
        }

        //Relational Properties

        public virtual CustomerProfile CustomerProfile { get; set; }

        public virtual ICollection<SoldTicket> SoldTickets { get; set; }
    }
}
