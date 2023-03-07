using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class SoldTicket : BaseEntity
    {
        public int TicketID { get; set; } //Bilet türünün ID'si
        public int CustomerID { get; set; } //Satın alan müşterinin ID'si (Kullanıcı girişi yapılmadan satılırsa ne olur?)
        public int SeanceID { get; set; } //Biletin satın alındığı seans bilgileri (Buradan salon,film bilgilerine de ulaşabiliriz.)
        public int BoxOfficeID { get; set; }

        //Relational Properties

        public virtual Ticket Ticket { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Seance Seance { get; set; }
        public virtual BoxOffice BoxOffice { get; set; }
    }
}
