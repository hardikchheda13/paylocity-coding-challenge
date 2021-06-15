using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BenefitsDataService.BenefitsData
{
    public class Beneficiary
    {
        [DataMember(Name = "firstName")]
        [Required, Column("FIRST_NAME")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        [Required, Column("LAST_NAME")]
        public string LastName { get; set; }

        [Required, Column("BENEFICIARY_TYPE_FK"), ForeignKey("BenficiaryType")]
        public int BeneficiaryTypeFK { get; set; }

        [Required, Column("EMPLOYER_FK"), ForeignKey("Employer")]
        public int EmployerFK { get; set; }

        public virtual BeneficiaryType  BeneficiaryType {get; set;}

        public virtual Employer Employer { get; set; }
    }
}
