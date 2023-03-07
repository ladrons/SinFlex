using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class BoxOffice : BaseEntity
    {
        public string Name { get; set; }

        //Relational Properties

        public virtual ICollection<SoldTicket> SoldTickets { get; set; }
    }
}
