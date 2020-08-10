using System;
using System.Collections.Generic;

namespace SmarterTomorrow.Models
{
    public  class Loan_in_dtl
    {
        public string VOUCHER_ID { get; set; }
        public int RECORD_ID { get; set; }
        
        public string EQUIPMENT_ID { get; set; }
        public int QUANTITY { get; set; }
        public DateTime ISSUE_DTE { get; set; }
        public DateTime RETURN_DTE { get; set; }


    }
}
