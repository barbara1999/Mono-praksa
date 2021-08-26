using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public class CityFilter
    {
        public string FilterValue { get; set; }

        public string SQlMethod()
        {
            if (FilterValue == null)
            {
                return String.Format("");
            }

            return String.Format(" WHERE NAME LIKE '%{0}%'", FilterValue);

        }
    }
}
