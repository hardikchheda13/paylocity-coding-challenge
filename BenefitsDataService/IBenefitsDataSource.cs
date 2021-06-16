using BenefitsDataService.BenefitsData;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitsDataService
{
    public interface IBenefitsDataSource : IEmployerDataService, IBeneficiaryDataSource, ICostDataSource, IDiscountRateDataSource
    {
    }
}
