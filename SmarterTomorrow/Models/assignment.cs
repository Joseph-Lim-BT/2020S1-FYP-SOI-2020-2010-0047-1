using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterTomorrow.Models
{
    public class assignment
    {
        public int ASSIGNMENT_ID { get; set; }
        public string EQUIPMENT_ID { get; set; }

        public string NRIC { get; set; }


        public DateTime DTE_TIME_CR { get; set; }


        public DateTime DTE_TIME_LAST_MOD { get; set; }


        public string CREATED_BY { get; set; }

        public string MODIFIED_BY { get; set; }

        public int ACTIVITY_ID { get; set; }

        public string PACKAGE_ID { get; set; }

    }
}
