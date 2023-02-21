using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Abstracts
{
    public interface ISeanceManager : IManager<Seance>
    {
        public bool CheckDates(Movie SaloonMovie, DateTime startDate, DateTime endDate);
        public List<DateTime> GenerateNewSeanceDate(DateTime startDate, DateTime endDate);

        public List<Seance> GenerateSeance(Saloon saloon, DateTime startDate, DateTime endDate, List<DateTime> seanceTimes);
    }
}
