using Project.ENTITIES.Models;

namespace Project.WebUI.Areas.Management.VMClasses
{
    public class BoxOfficeVM
    {
        public IQueryable<BoxOffice> BoxOffices { get; set; }

        public IQueryable<Movie> Movies { get; set; }
    }
}
