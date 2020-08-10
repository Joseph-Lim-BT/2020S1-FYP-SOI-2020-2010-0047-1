using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SmarterTomorrow.Models
{
    public class accessories
    {
        public string ACCESSORY_ID { get; set; }

        public string ACCESSORY_NAME { get; set; }

        public string ELEMENT_MATERIAL_NO { get; set; }

        public string SERIAL_NO { get; set; }

        public string STORAGE_LOCATION { get; set; }

        public string STORAGE_BIN { get; set; }

        [Required(ErrorMessage = "Please enter quantity")]
        [Range(0, 100, ErrorMessage = "Invalid number (0-100)")]
        public int QUANTITY { get; set; }

        [DataType(DataType.Date)]
        public DateTime DTE_TIME_CR { get; set; }

        [DataType(DataType.Date)]
        public DateTime DTE_TIME_LAST_MOD { get; set; }

        public string CREATED_BY { get; set; }

        public string MODIFIED_BY { get; set; }

        [Required(ErrorMessage = "Please enter Box Lot Number")]
        [RegularExpression("[0-9]{2}[A-Z]{2}", ErrorMessage = "Invalid Box Number (e.g. 13EX, 07AB)")]
        public string BOX_LOT_NO { get; set; }

        

        
    }
}
