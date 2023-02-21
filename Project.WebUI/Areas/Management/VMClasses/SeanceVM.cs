using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Project.DTO.Internal;
using Project.ENTITIES.Models;

namespace Project.WebUI.Areas.Management.VMClasses
{
    public class SeanceVM
    {
        public Seance Seance { get; set; }
        public SeanceDTO SeanceDTO { get; set; }

        public IQueryable<Seance> Seances { get; set; }

        //-------------------------------------//

        public Saloon Saloon { get; set; }

        //-------------------------------------//
    }
}
