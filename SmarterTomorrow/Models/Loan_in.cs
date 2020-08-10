using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmarterTomorrow.Models
{
    public  class Loan_in
    {

        public string VOUCHER_ID { get; set; }

        public string NRIC { get; set; }

        [Required(ErrorMessage = "Please enter Date/Time")]
        [DataType(DataType.DateTime)]
        [Remote(action: "VerifyDate", controller: "Loan")]

        public DateTime EXPECTED_RETURN_DTE { get; set; }
     
        public string REMARKS { get; set; }
        public DateTime DTE_TIME_CR { get; set; }
        public DateTime DTE_TIME_LAST_MOD { get; set; }
        public string CREATED_BY { get; set; }
        public string MODIFIED_BY { get; set; }

    }
}
