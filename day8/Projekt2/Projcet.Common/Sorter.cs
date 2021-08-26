using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public class Sorter
    {
        public string SortBy { get; set; }

        public string Order { get; set; } = "ASC";

        public string SQLMethod()
        {
            if (SortBy != null) 
            {
                return String.Format(" ORDER BY {0} {1}", SortBy, Order);
            }

            return String.Format("");
        }

    }
}
