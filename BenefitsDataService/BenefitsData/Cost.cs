using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BenefitsDataService.BenefitsData
{
    [DataContract]
    [Table("COST")]
    public class Cost
    {
        [DataMember(Name = "id")]
        [Key, Column("COST_PK")]
        public int ID { get; set; }

        [DataMember(Name = "amount")]
        [Required, Column("AMOUNT")]
        public double Amount { get; set; }

        [Required, Column("BENEFICIARY_TYPE_FK"), ForeignKey("BenficiaryType")]
        public int BeneficiaryTypeFK { get; set; }

        [Required, Column("EMPLOYER_FK"), ForeignKey("Employer")]
        public int EmployerFK { get; set; }

        public virtual BeneficiaryType BeneficiaryType { get; set; }

        public virtual Employer Employer { get; set; }
    }
}
