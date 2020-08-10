using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterTomorrow.Models
{
    public class activity
    {
        public int ACTIVITY_ID { get; set; }

        [Required(ErrorMessage = "Enter Brigade")]
        public string BRIGADE { get; set; }

        [Required(ErrorMessage = "Enter Type")]
        public string TYPE { get; set; }

        [Required(ErrorMessage = "Enter Activity Description")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "1-100 chars")]
        public string ACTIVITY_DESC { get; set; }

        [Required(ErrorMessage = "Enter Status")]
        public string STATUS { get; set; }
        [DataType(DataType.Date)]
        public DateTime ACTIVITY_START { get; set; }
        [DataType(DataType.Date)]
        public DateTime ACTIVITY_END { get; set; }

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
