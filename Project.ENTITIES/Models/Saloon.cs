using Project.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Saloon : BaseEntity, IDeletionInfo
    {
        public string Name { get; set; }
        public ushort Capacity { get; set; } //Koltuk sayısı.
        public string DimensionType { get; set; } //Salonun 2D mi yoksa 3D mi film gösterimi yaptığını belirtir.
        public bool IsOpen { get; set; } //Salonun açık yada kapalı olduğunu gösterir.

        public int? MovieID { get; set; }

        public string? ReasonForDelete { get; set; }
        public string? WhoDeleted { get; set; }


        //Relational Properties

        //[JsonIgnore]
        public virtual Movie Movie { get; set; }


        public virtual ICollection<Seance> Seances { get; set; }
    }
}