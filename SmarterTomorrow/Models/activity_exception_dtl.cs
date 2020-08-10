using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterTomorrow.Models
{
    public class activity_exception_dtl
    {

        public int RECORD_ID { get; set; }

        public int ACTIVITY_ID { get;set; }
        
        public string EQUIPMENT_ID { get; set; }



        [Required(ErrorMessage = "Enter Quantity")]
        public int QUANTITY { get; set; }

        public string STATUS { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "1-255 chars")]
        public string REMARKS { get; set; }
    }
}
