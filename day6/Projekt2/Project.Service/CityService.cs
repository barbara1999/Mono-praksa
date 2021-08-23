using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Model.Common;
using Project.Repository;
using Project.Repository.Common;
using Project.Service.Common;

namespace Project.Service
{
    public class CityService : ICityService
    {
        ICityRepository cityRepository = new CityRepository();

        public async Task<List<ICity>> GetAllCityAsync()
        {
            return await cityRepository.GetCitiesAsync();
        }

        public async Task<ICity> GetCityByIdAsync(int id)
        {
            return await cityRepository.GetCityByIdAsync(id);
        }

        public async Task<ICity> GetCityByNameAsync(string name)
        {
            return await cityRepository.GetCityByNameAsync(name);
        }

        public async Task<bool> CreateCityAsync(ICity city)
        {
            return await cityRepository.CreateCityAsync(city);
        }

        public async Task<bool> UpdateCityAsync(int id, ICity city)
        {
            return await cityRepository.UpdateCityAsync(id, city);
        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            return await cityRepository.DeleteCityAsync(id);
        }
    }
}
