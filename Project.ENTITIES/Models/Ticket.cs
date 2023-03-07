using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Ticket : BaseEntity
    {
        public string Name { get; set; } //Tam, Öğrenci, Promosyon vb.
        public string Type { get; set; } // 2D, 3D vb.
        public decimal? Price { get; set; } //Fiyat.

        //Relational Properties

        public virtual ICollection<SoldTicket> SoldTickets { get; set; }
    }
}
