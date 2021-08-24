using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Model.Common;
using Project.Repository.Common;

namespace Project.Repository
{
    public class CityRepository : ICityRepository
    {
        SqlConnection _connection;
        public CityRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ICity>> GetCitiesAsync()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM City", _connection);

            await _connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            List<ICity> cities = new List<ICity>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    City city = new City(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                    cities.Add(city);
                }
            }
            reader.Close();
            _connection.Close();
            return cities;
        }

        public async Task<ICity> GetCityByIdAsync(int id)
        {
            ICity city = new City();
            SqlCommand command = new SqlCommand($"SELECT * FROM City WHERE CityID={id} ", _connection);

            await _connection.OpenAsync();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    city = new City(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                }
            }

            reader.Close();
            _connection.Close();
            return city;
        }



        public async Task<ICity> GetCityByNameAsync(string name)
        {
            SqlCommand command = new SqlCommand($"SELECT * FROM City WHERE Name='{name}' ", _connection);
            ICity city = new City();
            await _connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    city = new City(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                }
            }

            reader.Close();
            _connection.Close();
            return city;
        }

        //moram napraviti da se ne smije kreirati grad sa istim imenom i poštanskim brojem koji će postoji u bazi
        public async Task<bool> CreateCityAsync(ICity city)
        {

            SqlCommand commandForID = new SqlCommand($"SELECT * FROM City WHERE CityID={city.CityID} ", _connection);
            await _connection.OpenAsync();

            SqlDataReader reader = await commandForID.ExecuteReaderAsync();

            if (city.CityID <= 0)
            {
                return false;
            }

            else if (reader.HasRows)
            {
                return false;
            }

            reader.Close();
            SqlCommand command = new SqlCommand($"INSERT INTO City (CityID, Name ,PostNumber) VALUES ({city.CityID},'{city.Name}',{city.PostNumber})", _connection);

            await command.ExecuteNonQueryAsync();
            _connection.Close();
            return true;
        }

        public async Task<bool> UpdateCityAsync(int id, ICity city)
        {


            SqlCommand commandForID = new SqlCommand($"SELECT * FROM City WHERE CityID={id} ", _connection);
            await _connection.OpenAsync();

            SqlDataReader reader = await commandForID.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            SqlCommand command = new SqlCommand($"UPDATE City SET  Name='{city.Name}', PostNumber='{city.PostNumber}' WHERE CityID={id}", _connection);
            command.ExecuteNonQuery();
            _connection.Close();
            return true;
        }

        public async Task<bool> DeleteCityAsync(int id)
        {

            SqlCommand commandForID = new SqlCommand($"SELECT * FROM City WHERE CityID={id} ", _connection);
            SqlCommand command = new SqlCommand();
            await _connection.OpenAsync();

            SqlDataReader reader = await commandForID.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {

                    command = new SqlCommand($"DELETE FROM City WHERE CityID={id}; ", _connection);
                }
            }

            else
            {
                return false;
            }
            reader.Close();
            await command.ExecuteNonQueryAsync();
            _connection.Close();
            return true;

        }



    }
}
