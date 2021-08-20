using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.Repository
{
    public class CityRepository
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["bascic"].ConnectionString);

        public async Task<List<City>> GetCitiesAsync()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM City", connection);

            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            List<City> cities = new List<City>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    City city = new City(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                    cities.Add(city);
                }
            }
            reader.Close();
            connection.Close();
            return cities;
        }

        public async Task<City> GetCityByIdAsync(int id)
        {
            City city = new City();
            SqlCommand command = new SqlCommand($"SELECT * FROM City WHERE CityID={id} ", connection);

            await connection.OpenAsync();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    city = new City(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                }
            }

            reader.Close();
            connection.Close();
            return city;
        }


        
        public async Task<City> GetCityByNameAsync(string name)
        {
            SqlCommand command = new SqlCommand($"SELECT * FROM City WHERE Name='{name}' ", connection);
            City city = new City();
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    city = new City(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                }
            }

            reader.Close();
            connection.Close();
            return city;
        }

        //moram napraviti da se ne smije kreirati grad sa istim imenom i poštanskim brojem koji će postoji u bazi
        public async Task<bool> CreateCityAsync(City city)
        {

            SqlCommand commandForID = new SqlCommand($"SELECT * FROM City WHERE CityID={city.CityID} ", connection);
            await connection.OpenAsync();

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
            SqlCommand command = new SqlCommand($"INSERT INTO City (CityID, Name ,PostNumber) VALUES ({city.CityID},'{city.Name}',{city.PostNumber})", connection);

            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;
        }

        public async Task<bool> UpdateCityAsync(int id, City city)
        {
 

            SqlCommand commandForID = new SqlCommand($"SELECT * FROM City WHERE CityID={id} ", connection);
            await connection.OpenAsync();

            SqlDataReader reader = await commandForID.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            SqlCommand command = new SqlCommand($"UPDATE City SET  Name='{city.Name}', PostNumber='{city.PostNumber}' WHERE CityID={id}", connection);
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }

        public async Task<bool> DeleteCityAsync(int id){

            SqlCommand commandForID = new SqlCommand($"SELECT * FROM City WHERE CityID={id} ", connection);
            SqlCommand command = new SqlCommand();
            await connection.OpenAsync();

            SqlDataReader reader = await commandForID.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {

                    command = new SqlCommand($"DELETE FROM City WHERE CityID={id}; ", connection);
                }
            }

            else
            {
                return false;
            }
            reader.Close();
            await command.ExecuteNonQueryAsync();
            connection.Close();
            return true;

        }



    }
}
