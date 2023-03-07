using Project.DTO.Internal;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Abstracts
{
    public interface IMovieManager : IManager<Movie>
    {
        Movie ConvertFromDTO(MovieDTO movieDTO, Genre genre);
        MovieDTO ConvertToDTO(Movie movie);



        Task<bool> CheckSameMovie(Movie movie);

        bool CheckIfAssigned(Movie movie);
    }
}
