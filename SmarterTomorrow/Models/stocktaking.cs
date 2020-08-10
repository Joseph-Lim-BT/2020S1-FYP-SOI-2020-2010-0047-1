using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterTomorrow.Models
{
    public class stocktaking
    {
        public int STOCKTAKE_ID { get; set; }

        [Required(ErrorMessage = "Enter Stocktake Type")]
        public string STOCKTAKE_TYPE { get; set; }


        public string NRIC { get; set; }

        public int ARCHIVE { get; set; }

        public int TOTAL_QUANTITY { get; set; }

        public int STOCKTAKE_QUANTITY { get; set; }

        [Required(ErrorMessage = "Enter Location")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "1-50 chars")]
        public string STOCKTAKE_LOCATION { get; set; }

        [Required(ErrorMessage = "Enter Bin")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "1-30 chars")]
        public string STOCKTAKE_BIN { get; set; }

        [DataType(DataType.Date)]
        public DateTime DTE_TIME_CR { get; set; }

        [DataType(DataType.Date)]
        public DateTime DTE_TIME_LAST_MOD { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "1-100 chars")]
        public string CREATED_BY { get; set; }

        public string MODIFIED_BY { get; set; }

        public int E_DIFF { get; set; }

        public int A_DIFF { get; set; }

        public int LOAN_IN_QTY { get; set; }

        public int LOAN_OUT_QTY { get; set; }

        public int DRAWN_OUT_QTY { get; set; }

        public int SEALED_QTY { get; set; }

        public int TOTAL_QUANTITY_ACC { get; set; }
        public int STOCKTAKE_QUANTITY_ACC { get; set; }

    }
}
