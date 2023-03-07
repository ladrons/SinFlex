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
    public class BoxOfficeManager : BaseManager<BoxOffice>, IBoxOfficeManager
    {
        IBoxOfficeRepository _boxOfficeRep;

        public BoxOfficeManager(IBoxOfficeRepository boxOfficeRep) : base(boxOfficeRep)
        {
            _boxOfficeRep = boxOfficeRep;
        }
    }
}
