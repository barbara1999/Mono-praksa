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

        public List<City> GetAllCity()
        {
            return cityRepository.GetCities();
        }

        public City GetCityById(int id)
        {
            return cityRepository.GetCityById(id);
        }

        public City GetCityByName(string name)
        {
            return cityRepository.GetCityByName(name);
        }

        public void CreateCity(City city)
        {
            cityRepository.CreateCity(city);
        }

        public bool UpdateCity(int id, City city)
        {
            return cityRepository.UpdateCity(id, city);
        }

        public bool DeleteCity(int id)
        {
            return cityRepository.DeleteCity(id);
        }
    }
}
