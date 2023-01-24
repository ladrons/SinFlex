using Project.BLL.ManagerServices.Abstracts;
using Project.DAL.Repositories.Abstracts;
using Project.DTO.Internal;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    public class MovieManager : BaseManager<Movie>, IMovieManager
    {
        IMovieRepository _movieRep;

        public MovieManager(IMovieRepository movieRep) : base(movieRep)
        {
            _movieRep = movieRep;
        }

        public Task<bool> CheckSameMovie(MovieDTO movieDTO)
        {
            return _movieRep.CheckSameMovie(movieDTO);
        }
    }
}
