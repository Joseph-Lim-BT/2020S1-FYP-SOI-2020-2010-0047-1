using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterTomorrow.Models
{
    public class stocktake_accessories
    {
        public int ASTOCKTAKE_ID { get; set; }
        public String ACCESSORY_NAME { get; set; }
        public String ACCESSORY_ID { get; set; }
        public String ELEMENT_MATERIAL_NO { get; set; }
        public String SERIAL_NO { get; set; }
        public String STORAGE_LOCATION { get; set; }
        public String STORAGE_BIN { get; set; }
        public int QUANTITY { get; set; }
        public String BOX_LOT_NO { get; set; }
    }
}
