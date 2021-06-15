using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace BenefitsDataService.BenefitsData
{
    [Table("NAME_STARTS_WITH_DISCOUNT_RATE")]
    public class NameStartsWithDiscountRate
    {
        [Key, Column("NAME_STARTS_WITH_DISCOUNT_RATE_PK")]
        public int ID { get; set; }

        [Required, Column("PREFIX_STRING")]
        public string PrefixString { get; set; }

        [Required, Column("RATE")]
        public decimal Rate { get; set; }
    }
}
