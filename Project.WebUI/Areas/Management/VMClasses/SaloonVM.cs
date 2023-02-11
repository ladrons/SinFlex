using Microsoft.AspNetCore.Mvc.Rendering;
using Project.DTO.Internal;
using Project.ENTITIES.Models;

namespace Project.WebUI.Areas.Management.VMClasses
{
    public class SaloonVM
    {
        public Saloon Saloon { get; set; }
        public IQueryable<Saloon> Saloons { get; set; }

        public Movie Movie { get; set; }
        public IQueryable<Movie> Movies { get; set; }

        public SaloonDTO SaloonDTO { get; set; }
        public MovieDTO MovieDTO { get; set; }

        public List<SelectListItem> MoviesSelectList { get; set; }
    }
}
