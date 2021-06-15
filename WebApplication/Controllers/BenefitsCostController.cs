using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using BenefitsService.BenefitsService;
using BenefitsDataService.BenefitsData;

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
        public IActionResult AddEmployee(BeneficiaryRequestData employee)
        {
            var json = _benefitsService.AddEmployee(employee);
            return Ok(json);
        }

        private HttpResponseMessage SendJsonData(string jsonData)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Content = new StringContent(jsonData, Encoding.Unicode);
            return response;
        }
    }
}