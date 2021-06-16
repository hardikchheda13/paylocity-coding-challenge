using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using BenefitsService.BenefitsService;
using BenefitsDataService.BenefitsData;
using BenefitsService;

namespace WebApplication.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BenefitsCostController : ControllerBase
    {
        private readonly IBenefitsService _benefitsService;
        
        public BenefitsCostController(IBenefitsService benefitsService)
        {
            _benefitsService = benefitsService;
        }

        [Route("employercost/{employerID}")]
        [HttpGet]
        public IActionResult GetEmployerCostData(int employerID)
        {
            var json = _benefitsService.CalculateBenefitsCostForGivenEmployer(employerID);
            return Ok(json);
        }

        [Route("employee")]
        [HttpPost]
        public IActionResult AddEmployee(BeneficiaryRequestData beneficiaryRequestData)
        {           
            if(!ValidateBeneficiaryRequestData(beneficiaryRequestData))
            {
                return BadRequest("whole or part of the request is missing");
            }
            var json = _benefitsService.AddNewEmployee(beneficiaryRequestData);
            return Ok(json);
        }

        private bool ValidateBeneficiaryRequestData(BeneficiaryRequestData beneficiaryRequestData)
        {
            if(beneficiaryRequestData == null || beneficiaryRequestData.FirstName == null || beneficiaryRequestData.FirstName == string.Empty)
            {
                return false;
            }

            return true;
        }
    }
}