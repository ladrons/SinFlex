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
    public class MovieFormatManager : BaseManager<MovieFormat>, IMovieFormatManager
    {
        IMovieFormatRepository _movieFormatRep;

        public MovieFormatManager(IMovieFormatRepository movieFormatRep ) : base(movieFormatRep)
        {
            _movieFormatRep = movieFormatRep;
        }
    }
}
