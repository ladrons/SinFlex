using Project.ENTITIES.Models;
using Project.WebUI.Areas.Management.Models;

namespace Project.WebUI.Areas.Management.VMClasses
{
    public class BoxOfficeVM
    {
        public SeanceInfoDTO SeanceInfoDTO { get; set; }

        public IQueryable<BoxOffice> BoxOffices { get; set; }

        public IQueryable<Movie> Movies { get; set; }


        public string MovieName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
