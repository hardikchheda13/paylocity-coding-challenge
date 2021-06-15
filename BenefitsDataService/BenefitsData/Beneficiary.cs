using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BenefitsDataService.BenefitsData
{
    [DataContract]
    public class Beneficiary
    {
        [DataMember(Name = "firstName")]
        [Required, Column("FIRST_NAME")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        [Required, Column("LAST_NAME")]
        public string LastName { get; set; }

        [Required, Column("BENEFICIARY_TYPE_FK"), ForeignKey("BeneficiaryType")]
        public int BeneficiaryTypeFK { get; set; }

        [Required, Column("EMPLOYER_FK"), ForeignKey("Employer")]
        public int EmployerFK { get; set; }

        public virtual BeneficiaryType  BeneficiaryType {get; set;}

        public virtual Employer Employer { get; set; }
    }

    [DataContract]
    public class BeneficiaryRequestData
    {
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "employerID")]
        public int EmployerID { get; set; }

        [DataMember(Name = "dependents")]
        public BeneficiaryRequestData[] Dependents { get; set; }
    }
}
