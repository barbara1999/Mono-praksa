using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Common
{
    public interface ICity
    {
        int CityID { get; set; }
        string Name { get; set; }
        int PostNumber { get; set; }
    }
}
