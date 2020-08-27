using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterTomorrow.Models
{
   public class Equipment
   {

      public string EQUIPMENT_ID { get; set; }

      public string SERIAL_NO { get; set; }

      public int EQUIPMENT_TYPE_ID { get; set; }

      public string ELEMENT_MATERIAL_NO { get; set; }

      public string STORAGE_LOCATION { get; set; }

      public string STORAGE_BIN { get; set; }

      public int STATUS { get; set; }

      [Required(ErrorMessage = "Please enter Box Lot Number")]
      [RegularExpression("[0-9]{2}[A-Z]{2}", ErrorMessage = "Invalid Box Number (e.g. 13EX, 07AB)")]
      public string BOX_LOT_NO { get; set; }

      public string BUTT_NO { get; set; }
      [Required(ErrorMessage = "Please enter quantity")]
      [Range(0,100, ErrorMessage = "Invalid number (0-100)")]
      public int QUANTITY { get; set; }

      public string CREATED_BY { get; set; }

      public string MODIFIED_BY { get; set; }

      [DataType(DataType.Date)]
      public DateTime DTE_TIME_CR { get; set; }

      [DataType(DataType.Date)]
      public DateTime DTE_TIME_LAST_MOD { get; set; }

        public string EQUIPMENT_NAME { get; set; }
    }
}
