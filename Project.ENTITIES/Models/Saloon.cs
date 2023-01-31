using Project.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Saloon : BaseEntity, IDeletionInfo
    {
        public string? Name { get; set; }
        public ushort Capacity { get; set; } //Koltuk sayısı

        public int? MovieID { get; set; }

        public string? ReasonForDelete { get; set; }
        public string? WhoDeleted { get; set; }

        //Relational Properties

        public virtual Movie Movie { get; set; }

    }
}