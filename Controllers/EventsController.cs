using Microsoft.AspNetCore.Mvc;
using OptimaTrackerWebService.Models;
using OptimaTrackerWebService.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OptimaTrackerWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IDatabaseService service;
        public EventsController(IDatabaseService databaseService)
        {
            service = databaseService;
        }
        // POST api/<EventsController>
        [HttpPost]
        public void Post([FromBody] Company data)
        {
            service.Insert(data);
        }
    }
}
