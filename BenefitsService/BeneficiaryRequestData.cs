using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BenefitsService
{
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
