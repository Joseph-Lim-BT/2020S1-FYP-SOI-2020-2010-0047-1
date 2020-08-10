using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterTomorrow.Models
{
    public class stocktake_equipment
    {
        public int ESTOCKTAKE_ID { get; set; }
        public String EQUIPMENT_ID { get; set; }
        public String EQUIPMENT_NAME { get; set; }
        public int EQUIPMENT_TYPE_ID { get; set; }
        public String ELEMENT_MATERIAL_NO { get; set; }
        public String SERIAL_NO { get; set; }
        public String STORAGE_LOCATION { get; set; }
        public String STORAGE_BIN { get; set; }
        public int QUANTITY { get; set; }
        public String BOX_LOT_NO { get; set; }
        public String BUTT_NO {get;set;}
        public int STATUS { get; set; }

    }
}
