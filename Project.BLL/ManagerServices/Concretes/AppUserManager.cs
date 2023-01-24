﻿using Project.BLL.ManagerServices.Abstracts;
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
    public class AppUserManager : BaseManager<AppUser>, IAppUserManager
    {
        IAppUserRepository _appRep;

        public AppUserManager(IAppUserRepository appRep) : base(appRep)
        {
            _appRep = appRep;
        }

        public Task<AppUser> GetFoundUser(AppUserDTO appUserDTO)
        {
            return _appRep.GetFoundUser(appUserDTO);
        }
    }
}
