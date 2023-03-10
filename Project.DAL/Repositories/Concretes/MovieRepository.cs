using Microsoft.EntityFrameworkCore;
using Project.DAL.Context;
using Project.DAL.Repositories.Abstracts;
using Project.DTO.Internal;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Concretes
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        private readonly MyContext _db;
        public MovieRepository(MyContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// Kullanıcı tarafından girilen bilgilerle aynı bir film var mı diye kontrol eder (Film adı ve süre üzerinden kontrol yapar)
        /// </summary>
        /// <param name="movieDTO">Db'de kontrol edilecek film</param>
        /// <returns>Eğer 'True' değer dönerse girilen bilgilere eş değer veri yoktur. 'False' değer dönerse girilen bilgilere eş değer vardır.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> CheckSameMovie(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));

            Movie foundMovie = await _db.Movies.FirstOrDefaultAsync(x => x.Title == movie.Title && x.Content == movie.Content);
            if (foundMovie == null)
            {
                return true;
            }
            else { return false; }
        }
    }
}
