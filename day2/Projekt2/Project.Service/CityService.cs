using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Repository;

namespace Project.Service
{
    public class CityService
    {
        CityRepository cityRepository = new CityRepository();

        public async Task<List<City>> GetAllCityAsync()
        {
            return await cityRepository.GetCitiesAsync();
        }

        public async Task<City> GetCityByIdAsync(int id)
        {
            return await cityRepository.GetCityByIdAsync(id);
        }

        public async Task<City> GetCityByNameAsync(string name)
        {
            return await cityRepository.GetCityByNameAsync(name);
        }

        public async Task<bool> CreateCityAsync(City city)
        {
            return await cityRepository.CreateCityAsync(city);
        }

        public async Task<bool> UpdateCityAsync(int id, City city)
        {
            return await cityRepository.UpdateCityAsync(id, city);
        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            return await cityRepository.DeleteCityAsync(id);
        }
    }
}
