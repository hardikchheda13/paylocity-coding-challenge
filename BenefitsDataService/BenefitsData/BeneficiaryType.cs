using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BenefitsDataService.BenefitsData
{
    [Table("BENEFICIARY_TYPES")]
    public class BeneficiaryType
    {
        [Key, Column("BENEFICIARY_TYPE_PK")]
        public int ID { get; set; }

        [Column("BENEFICIARY_TYPE"), Required]
        public string Person_Type { get; set; }
    }
}