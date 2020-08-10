using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterTomorrow.Models
{
    public class User_Grp
    {
        public int GROUP_ID { get; set; }

        public string GROUP_NAME { get; set; }

        public DateTime DTE_TIME_CR { get; set; }

        public DateTime DTE_TIME_LAST_MOD { get; set; }

        public string CREATED_BY { get; set; }

        public string MODIFIED_BY { get; set; }
    }
}
