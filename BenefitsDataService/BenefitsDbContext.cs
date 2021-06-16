using BenefitsDataService.BenefitsData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitsDataService
{
    public class BenefitsDbContext : DbContext
    {
        public BenefitsDbContext(DbContextOptions<BenefitsDbContext> options)
        : base(options)
        {

        }

        public DbSet<Employer> Employers { get; set; }
        public DbSet<BeneficiaryType> BeneficiaryTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<NameStartsWithDiscountRate> NameStartsWithDiscountRates { get; set; } 
    }
}