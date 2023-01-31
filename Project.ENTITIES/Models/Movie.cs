using Project.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Movie : BaseEntity, IDeletionInfo
    {
        public string? Title { get; set; } //Başlık
        public string? Content { get; set; } //Film içeriği/konusu
        public string? Duration { get; set; } //Süre
        public DateTime ReleaseDate { get; set; } //Vizyona giriş tarihi

        public int GenreID { get; set; }

        public string? ReasonForDelete { get; set; }
        public string? WhoDeleted { get; set; }

        //Relational Properties

        public virtual Genre Genre { get; set; }

        public virtual ICollection<Saloon> Saloons { get; set; }
    }
}
