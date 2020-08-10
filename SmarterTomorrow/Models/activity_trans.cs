using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterTomorrow.Models
{
    public class activity_trans
    {

        [StringLength(22, MinimumLength = 22, ErrorMessage = "22 chars only")]
        public string TRANS_ID { get; set; }

        public int ACTIVITY_ID { get; set; }

        [Required(ErrorMessage = "Enter your NRIC")]
        [RegularExpression("[STFG]\\d{7}[A-Z]", ErrorMessage = "Invalid NRIC format")]
        public string NRIC { get; set; }


        public string TYPE { get; set; }



        public string MACHINE_NAME { get; set; }


        [DataType(DataType.Date)]
        public DateTime DTE_TIME_CR { get; set; }


        [DataType(DataType.Date)]
        public DateTime DTE_TIME_LAST_MOD { get; set; }


        public string CREATED_BY { get; set; }

   
        public string MODIFIED_BY { get; set; }
}
}
