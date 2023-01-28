using Project.DTO.Internal;
using Project.ENTITIES.Models;

namespace Project.WebUI.Areas.Management.VMClasses
{
    public class MovieVM
    {
        public Movie Movie { get; set; }
        public IQueryable<Movie> Movies { get; set; }

        public Genre Genre { get; set; }
        public IQueryable<Genre> Genres { get; set; }

        public MovieDTO MovieDTO { get; set; }
    }
}
