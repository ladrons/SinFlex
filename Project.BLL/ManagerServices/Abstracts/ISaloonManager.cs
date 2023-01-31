using Project.DTO.Internal;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Abstracts
{
    public interface ISaloonManager : IManager<Saloon>
    {
        Saloon ConvertFromDTO(SaloonDTO saloonDTO);
        SaloonDTO ConvertToDTO(Saloon saloon);

    }
}
