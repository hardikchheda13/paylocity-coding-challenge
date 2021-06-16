using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BenefitsDataService.BenefitsData
{
    [Table("EMPLOYEES")]
    public class Employee : Beneficiary
    {
        [Key, Column("EMPLOYEE_PK")]
        public int ID { get; set; }

        public virtual ICollection<Dependent> Dependents { get; set; }
    }
}
