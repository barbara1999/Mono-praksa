using Project.Common;
using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface ICityService
    {
        Task<bool> CreateCityAsync(ICity city);
        Task<bool> DeleteCityAsync(int id);
        Task<List<ICity>> GetAllCityAsync(Sorter sorter,CityFilter city, Pager pager);
        Task<ICity> GetCityByIdAsync(int id);
        Task<ICity> GetCityByNameAsync(string name);
        Task<bool> UpdateCityAsync(int id, ICity city);
    }
}
