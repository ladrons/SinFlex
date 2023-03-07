using Project.ENTITIES.Models;

namespace Project.WebUI.VMClasses
{
    public class SoldTicketVM
    {
        public List<Movie> ActiveMovies { get; set; }
        public List<Seance> ActiveSeances { get; set; }
    }
}
