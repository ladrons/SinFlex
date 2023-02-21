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
    public class SaloonManager : BaseManager<Saloon>, ISaloonManager
    {
        ISaloonRepository _saloonRep;

        public SaloonManager(ISaloonRepository saloonRep) : base(saloonRep)
        {
            _saloonRep = saloonRep;
        }

        public Saloon ConvertFromDTO(SaloonDTO saloonDTO)
        {
            Saloon saloon = new Saloon
            {
                Name = saloonDTO.Name,
                Capacity = Convert.ToUInt16(saloonDTO.Capacity),
                DimensionType = saloonDTO.DimensionType,
                IsOpen = (saloonDTO == null) ? true : saloonDTO.IsOpen
            };
            return saloon;
        }

        public SaloonDTO ConvertToDTO(Saloon saloon)
        {
            SaloonDTO saloonDTO = new SaloonDTO
            {
                ID = saloon.ID.ToString(),
                Name = saloon.Name,
                Capacity = saloon.Capacity,
                DimensionType = saloon.DimensionType,
                IsOpen = saloon.IsOpen
            };
            return saloonDTO;
        }
    }
}
