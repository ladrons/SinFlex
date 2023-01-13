using Project.DTO.Internal;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Abstracts
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        //Aktif olanlar
        Task<AppUser> GetFoundUser(AppUserDTO appUserDTO);


        //Pasif olanlar
        Task<AppUser> FindUserAsync(AppUser appUser);
        Task<AppUser> GetFoundUser(AppUser appUser);
    }
}
