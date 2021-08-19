using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
   public class City
    {
        public City()
        {
        }

        public City(int mId, string mName, int mPostNumber)
        {
            CityID = mId;
            Name = mName;
            PostNumber = mPostNumber;

        }

        public int CityID { get; set; }
        public string Name { get; set; }
        public int PostNumber { get; set; }
    }
}
