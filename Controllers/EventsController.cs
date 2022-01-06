using Microsoft.AspNetCore.Mvc;
using OptimaTrackerWebService.Models;
using OptimaTrackerWebService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OptimaTrackerWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        // POST api/<EventsController>
        [HttpPost]
        public void Post([FromBody] CompanyModel companyData)
        {
            var databaseService = new DatabaseService();
            databaseService.Insert(companyData);
        }

        [HttpGet]
        public string Get()
        {
            return "123";
        }
    }
}
