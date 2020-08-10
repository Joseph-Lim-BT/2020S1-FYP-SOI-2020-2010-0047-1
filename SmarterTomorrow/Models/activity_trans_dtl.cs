using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterTomorrow.Models
{
    public class activity_trans_dtl
    {
        public int RECORD_ID { get; set; }


        public string TRANS_ID { get; set; }


        [StringLength(20, MinimumLength = 1, ErrorMessage = "1-20 chars")]
        public string ELEMENT_MATERIAL_NO { get; set; }

 
        [StringLength(100, MinimumLength = 1, ErrorMessage = "1-100 chars")]
        public string ELEMENT_MATERIAL_DESC { get; set; }

        [Required(ErrorMessage = "Enter Full Description")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "1-100 chars")]
        public string FULL_DESC { get; set; }

        [Required(ErrorMessage = "Enter Short Description")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "1-50 chars")]
        public string SHORT_DESC { get; set; }


        [StringLength(20, MinimumLength = 1, ErrorMessage = "1-20 chars")]
        public string SERIAL_NO { get; set; }


        [StringLength(50, MinimumLength = 1, ErrorMessage = "1-50 chars")]
        public string BOM_NO { get; set; }


        [StringLength(50, MinimumLength = 1, ErrorMessage = "1-50 chars")]
        public string STORAGE_LOCATION { get; set; }


        [StringLength(30, MinimumLength = 1, ErrorMessage = "1-30 chars")]
        public string STORAGE_BIN { get; set; }

 
        [StringLength(10, MinimumLength = 1, ErrorMessage = "1-10 chars")]
        public string BOX_LOT_NO { get; set; }

        public int QUANTITY { get; set; }
    }
}
