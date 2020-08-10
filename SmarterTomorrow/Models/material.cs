using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmarterTomorrow.Models
{
    public class material
    {

        [Required(ErrorMessage = "Enter Element Material Number")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "1-20 chars")]
        public string ELEMENT_MATERIAL_NO { get; set; }

        [Required(ErrorMessage = "Enter Element Material Description")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "1-100 chars")]
        public string ELEMENT_MATERIAL_DESC { get; set; }

        [Required(ErrorMessage = "Enter Group NSN Number")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "1-30 chars")]
        public string GROUP_NSN_NO { get; set; }

        [Required(ErrorMessage = "Enter Group NSN Description")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "1-50 chars")]
        public string GROUP_NSN_DESC { get; set; }

        [Required(ErrorMessage = "Enter Material Type")]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "1-15 chars")]
        public string MATERIAL_TYPE { get; set; }

        [Required(ErrorMessage = "Enter Material Unit")]
        public string MATERIAL_UNIT { get; set; }
        public DateTime DTE_TIME_CR { get; set; }
        public DateTime DTE_TIME_LAST_MOD { get; set; }
        public string CREATED_BY { get; set; }
        public string MODIFIED_BY { get; set; }

    }
}
