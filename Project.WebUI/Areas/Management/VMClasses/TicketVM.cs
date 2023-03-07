using Project.DTO.Internal;
using Project.ENTITIES.Models;

namespace Project.WebUI.Areas.Management.VMClasses
{
    public class TicketVM
    {
        public Ticket Ticket { get; set; }
        public TicketDTO TicketDTO { get; set; }
        public IQueryable<Ticket> Tickets { get; set; }
    }
}
