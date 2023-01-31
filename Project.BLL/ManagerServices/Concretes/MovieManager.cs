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

        //-------------------------------------\\

        public Movie ConvertFromDTO(MovieDTO movieDTO, Genre genre)
        {
            Movie movie = new Movie
            {
                Title = movieDTO.Title,
                Content = movieDTO.Content,
                Genre = genre,
                Duration = movieDTO.Duration,
                ReleaseDate = movieDTO.ReleaseDate,
            };
            return movie;
        }
        public MovieDTO ConvertToDTO(Movie movie)
        {
            MovieDTO movieDTO = new MovieDTO
            {
                ID = movie.ID.ToString(),
                Title = movie.Title,
                Content = movie.Content,
                Genre = movie.Genre.Name.ToString(),
                Duration = movie.Duration,
                ReleaseDate = movie.ReleaseDate,
            };
            return movieDTO;
        }

        //-------------------------------------\\

        public Task<bool> CheckSameMovie(MovieDTO movieDTO)
        {
            return _movieRep.CheckSameMovie(movieDTO);
        }
    }
}
