using Project.DTO.Internal;
using Project.ENTITIES.Models;

namespace Project.WebUI.Areas.Management.VMClasses
{
    public class SaloonVM
    {
        public Saloon Saloon { get; set; }
        public IQueryable<Saloon> Saloons { get; set; }

        public SaloonDTO SaloonDTO { get; set; }
    }
}
