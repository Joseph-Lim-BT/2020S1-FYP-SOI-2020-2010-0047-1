using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterTomorrow.Models
{
    public class User
    {
        [Required(ErrorMessage = "Enter your NRIC")]
        [RegularExpression("[STFG]\\d{7}[A-Z]", ErrorMessage = "Invalid NRIC")]
        public string NRIC { get; set; }

        public string PASSWORD { get; set; }

        [Required(ErrorMessage = "Enter Full name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "1-50 chars")]
        public string FULL_NAME { get; set; }
        public string RANK { get; set; }
        public string UNIT { get; set; }
        public string PERSON_ID { get; set; }
        public int GROUP_ID { get; set; }
        public DateTime DTE_TIME_CR { get; set; }
        public DateTime DTE_TIME_LAST_LOGIN { get; set; }
        public DateTime DTE_TIME_LAST_MOD { get; set; }
        public string CREATED_BY { get; set; }
        public string MODIFIED_BY { get; set; }

        public bool RememberMe { get; set; }
    }
}
