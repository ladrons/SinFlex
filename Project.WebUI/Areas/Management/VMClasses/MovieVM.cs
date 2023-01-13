using Project.DTO.Internal;
using Project.ENTITIES.Models;

namespace Project.WebUI.Areas.Management.VMClasses
{
    public class MovieVM
    {
        public MovieDTO MovieDTO { get; set; }
        public IQueryable<Genre> Genres { get; set; }
    }
}
