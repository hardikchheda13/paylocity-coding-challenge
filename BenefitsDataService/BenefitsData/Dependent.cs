using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace BenefitsDataService.BenefitsData
{
    [Table("DEPENDENTS")]
    public class Dependent : Beneficiary
    {
        [Key, Column("EMPLOYEE_PK")]
        public int ID { get; set; }

        [Required, Column("EMPLOYEE_FK"), ForeignKey("Employee")]
        public int EmployeeFK { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
