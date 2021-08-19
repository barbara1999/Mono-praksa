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

        public List<City> GetCities()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM City", connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<City> cities = new List<City>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    City city = new City(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                    cities.Add(city);
                }
            }
            reader.Close();
            connection.Close();
            return cities;
        }

        public City GetCityById(int id)
        {
            City city = new City();
            SqlCommand command = new SqlCommand($"SELECT * FROM City WHERE CityID={id} ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    city = new City(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                }
            }

            reader.Close();
            connection.Close();
            return city;
        }

        public City GetCityByName(string name)
        {
            SqlCommand command = new SqlCommand($"SELECT * FROM City WHERE Name='{name}' ", connection);
            City city = new City();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                city = new City(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
            }
            return city;
        }

        public void CreateCity(City city)
        {
            City mCity = new City();
            mCity.CityID = city.CityID;
            mCity.Name = city.Name;
            mCity.PostNumber = city.PostNumber;
            SqlCommand command = new SqlCommand($"INSERT INTO City (CityID, Name ,PostNumber) VALUES ({mCity.CityID},'{mCity.Name}',{mCity.PostNumber})", connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public bool UpdateCity(int id, City city)
        {
            City mCity = new City();
            mCity.CityID = city.CityID;
            mCity.Name = city.Name;
            mCity.PostNumber = city.PostNumber;
            

            SqlCommand commandForID = new SqlCommand($"SELECT * FROM City WHERE CityID={id} ", connection);
            connection.Open();

            SqlDataReader reader = commandForID.ExecuteReader();

            if (!reader.HasRows)
            {
                return false;
            }

            reader.Close();

            SqlCommand command = new SqlCommand($"UPDATE City SET  Name='{mCity.Name}', PostNumber='{mCity.PostNumber}' WHERE CityID={id}", connection);
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }

        public bool DeleteCity(int id){

            SqlCommand commandForID = new SqlCommand($"SELECT * FROM City WHERE CityID={id} ", connection);
            SqlCommand command = new SqlCommand();
            connection.Open();

            SqlDataReader reader = commandForID.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    command = new SqlCommand($"DELETE FROM City WHERE CityID={id}; ", connection);
                }
            }

            else
            {
                return false;
            }
            reader.Close();
            command.ExecuteNonQuery();
            connection.Close();
            return true;

        }



    }
}
