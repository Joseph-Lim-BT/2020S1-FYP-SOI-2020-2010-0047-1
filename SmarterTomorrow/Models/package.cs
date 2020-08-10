using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterTomorrow.Models
{
    public class package
    {
        public string PACKAGE_ID { get; set; }

        public string EQUIPMENT_ID { get; set; }

        public string ACCESSORY_ID { get; set; }

        public string PACKAGE_TYPE { get; set; }

        public string PACKAGE_DESC { get; set; }

        [DataType(DataType.Date)]
        public DateTime DTE_TIME_CR { get; set; }

        [DataType(DataType.Date)]
        public DateTime DTE_TIME_LAST_MOD { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "1-100 chars")]
        public string CREATED_BY { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "1-100 chars")]
        public string MODIFIED_BY { get; set; }
    }
}
