using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Seance : BaseEntity
    {
        public DateTime Date { get; set; } //Seansın tarih bilgisi
        public string Time { get; set; } //Seansın saat bilgisi

        public int SaloonID { get; set; }


        //Relational Properties

        //[JsonIgnore]
        public virtual Saloon Saloon { get; set; }


        public virtual ICollection<SoldTicket> SoldTickets { get; set; }
    }
}
