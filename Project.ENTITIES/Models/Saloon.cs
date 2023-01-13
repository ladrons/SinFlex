using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Saloon : BaseEntity
    {
        public string? SaloonName { get; set; }
        public ushort Capacity { get; set; } //Koltuk sayısı


        //Relational Properties


    }
}