using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Interfaces
{
    public interface IDeletionInfo
    {
        //Bu Interface bazı modellerde silme işlemini gerçekleştiren personelin bilgisini ve neden sildiğini bildiren 2 adet property barındıracaktır.

        public string ReasonForDelete { get; set; } //Silme nedeni.
        public string WhoDeleted { get; set; } //Silen kişi bilgisi.
    }
}
