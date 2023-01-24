using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Movie : BaseEntity
    {
        public string? Title { get; set; } //Başlık
        public string? Content { get; set; } //Film içeriği/konusu
        public string? Duration { get; set; } //Süre
        public DateTime ReleaseDate { get; set; } //Vizyona giriş tarihi

        //Relational Properties

        public virtual Genre Genre { get; set; }
    }
}
