using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BenefitsDataService.BenefitsData
{
    public class Beneficiary
    {
        [Required, Column("FIRST_NAME")]
        public string FirstName { get; set; }

        [Required, Column("LAST_NAME")]
        public string LastName { get; set; }

        [Required, Column("BENEFICIARY_TYPE_FK"), ForeignKey("BeneficiaryType")]
        public int BeneficiaryTypeFK { get; set; }

        [Required, Column("EMPLOYER_FK"), ForeignKey("Employer")]
        public int EmployerFK { get; set; }

        public virtual BeneficiaryType  BeneficiaryType {get; set;}

        public virtual Employer Employer { get; set; }
    }
}
