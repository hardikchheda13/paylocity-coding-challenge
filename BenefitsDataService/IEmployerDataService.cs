using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitsDataService
{
    public interface IEmployerDataService
    {
        public bool IsEmployerAvailable(int employerID);
        public void AddNewEmployer(int employerID);
    }
}
