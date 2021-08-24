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
        ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<List<ICity>> GetAllCityAsync()
        {
            return await _cityRepository.GetCitiesAsync();
        }

        public async Task<ICity> GetCityByIdAsync(int id)
        {
            return await _cityRepository.GetCityByIdAsync(id);
        }

        public async Task<ICity> GetCityByNameAsync(string name)
        {
            return await _cityRepository.GetCityByNameAsync(name);
        }

        public async Task<bool> CreateCityAsync(ICity city)
        {
            return await _cityRepository.CreateCityAsync(city);
        }

        public async Task<bool> UpdateCityAsync(int id, ICity city)
        {
            return await _cityRepository.UpdateCityAsync(id, city);
        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            return await _cityRepository.DeleteCityAsync(id);
        }
    }
}
