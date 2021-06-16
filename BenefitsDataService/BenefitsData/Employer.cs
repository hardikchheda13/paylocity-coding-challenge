using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BenefitsDataService.BenefitsData
{
    [DataContract]
    [Table("EMPLOYERS")]
    public class Employer
    {
        [DataMember(Name = "id")]
        [Key, Column("EMPLOYER_PK")]        
        public int ID { get; set; }

        [DataMember(Name = "name")]
        [Column("NAME"), Required]
        public string Name { get; set; }
    }
}
