﻿using Project.BLL.ManagerServices.Abstracts;
using Project.DAL.Repositories.Abstracts;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    public class GenreManager : BaseManager<Genre>, IGenreManager
    {
        IGenreRepository _genreRep;

        public GenreManager(IGenreRepository genreRep) : base(genreRep)
        {
            _genreRep = genreRep;
        }
    }
}
