using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
   public class Pager
    {
        public int PageNumber { get; set; } 
        public int PageSize { get; set; }

        public string getSQL()
        {
            
            if(PageNumber==0 && PageSize == 0)
            {
                return String.Format("");
            }

            int Offset = (PageNumber - 1) * PageSize;

            return String.Format(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY",Offset,PageSize);
        }
    }
}
