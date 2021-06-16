using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BenefitsDataService.BenefitsData
{
    [DataContract]
    public class BeneficiaryCostData
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "cost")]
        public double Cost { get; set; }

        [DataMember(Name = "beneficiaryType")]
        public string BeneficiaryType { get; set; }
    }

    [DataContract]
    public class EmployeeCostData : BeneficiaryCostData
    {
        [DataMember(Name = "Dependents")]
        public List<DependentCostData> DependentCostData { get; set; }
    }

    [DataContract]
    public class DependentCostData : BeneficiaryCostData
    {

    }

    [DataContract]
    public class EmployerCostData
    {
        [DataMember(Name = "EmployeeCostData")]
        public List<EmployeeCostData> EmployeeCostDatas { get; set; }

        [DataMember(Name = "TotalCost")]
        public double TotalCost { get; set; }
    }
}
