using Project.BLL.ManagerServices.Abstracts;
using Project.DAL.Repositories.Abstracts;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    public class SeanceManager : BaseManager<Seance>, ISeanceManager
    {
        ISeanceRepository _seanceRep;

        public SeanceManager(ISeanceRepository seanceRep) : base(seanceRep)
        {
            _seanceRep = seanceRep;
        }

        //-------------------------------------//

        public bool CheckDates(Movie saloonMovie, DateTime startDate, DateTime endDate)
        {
            if (startDate >= saloonMovie.ReleaseDate)
            {
                if (endDate > startDate)
                    return true;
            }
            return false;
        }

        //-------------------------------------//

        public List<DateTime> GenerateNewSeanceDate(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dateTimes = new List<DateTime>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                dateTimes.Add(date);
            }
            return dateTimes;
        }

        //-------------------------------------//

        public List<Seance> GenerateSeance(Saloon saloon, DateTime startDate, DateTime endDate, List<DateTime> seanceTimes)
        {
            if (CheckDates(saloon.Movie, startDate, endDate))
            {
                List<Seance> newSeances = new List<Seance>();

                List<DateTime> dates = GenerateNewSeanceDate(startDate, endDate);
                for (int i = 0; i < dates.Count; i++)
                {
                    for (int j = 0; j < seanceTimes.Count; j++)
                    {
                        Seance seance = new Seance();

                        seance.Date = dates[i];
                        seance.Time = seanceTimes[j].ToShortTimeString();
                        seance.SaloonID = saloon.ID;

                        newSeances.Add(seance);
                    }
                }
                return newSeances;
            }
            return null;
        }

        //-------------------------------------//
    }
}
