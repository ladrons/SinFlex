using Microsoft.EntityFrameworkCore;
using Project.DAL.Context;
using Project.DAL.Repositories.Abstracts;
using Project.DTO.Internal;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Concretes
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        private readonly MyContext _db;

        public AppUserRepository(MyContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// Kendisine gelen DTO değerini DB'de sorgular ve denk geldiği 'AppUser' nesnesini döndürür.
        /// </summary>
        /// <param name="appUserDTO">Gelen AppUserDTO değeri.</param>
        /// <returns>DTO değerine karşılık gelen 'AppUser' nesnesi.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<AppUser> GetFoundUser(AppUserDTO appUserDTO)
        {
            if (appUserDTO == null)
                throw new ArgumentNullException(nameof(appUserDTO));

            AppUser? foundUser = await _db.AppUsers.FirstOrDefaultAsync(x => x.Username == appUserDTO.Username && x.Password == appUserDTO.Password);

            if (foundUser == null)
                throw new ArgumentNullException(nameof(foundUser));
            return foundUser;
        }

        public async Task<AppUser> FindUserAsync(AppUser appUser)
        {
            //if (appUser == null) //ToDo: Bu kontrolü Validation eklediğim de kaldıracağım. (appUser == null)
            //    throw new ArgumentNullException(nameof(appUser));

            //AppUser foundUser = await _db.AppUsers.FirstOrDefaultAsync(x => x.Username == appUser.Username && x.Password == appUser.Password);

            //if (foundUser == null)
            //    throw new ArgumentNullException(nameof(foundUser));

            //return foundUser;
            throw new NotImplementedException();
        }
        public Task<AppUser> GetFoundUser(AppUser appUser)
        {
            throw new NotImplementedException();
        }
    }
}
