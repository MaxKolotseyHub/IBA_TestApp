using IBA_Test_BLL.Interfaces;
using IBA_Test_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;

namespace IBA_Test.Controllers
{
    public class DriversController : ApiController
    {
        private readonly IDriversService _driversService;

        public DriversController(IDriversService driversService)
        {
            _driversService = driversService;
        }
        [Route("drivers/datespeed")]
        [HttpPost]
        public async Task<IHttpActionResult> GetByDateAndSpeed([FromBody] DriverFilterViewModel model)
        {
            if (ModelState.IsValid)
            {
               var drivers = await _driversService.GetByDateAndSpeed(model.DateTime, model.Speed);
               return Ok(drivers);
            }
            else return BadRequest();
        }

        [Route("drivers/date")]
        [HttpPost]
        public async Task<IHttpActionResult> GetByDate([FromBody] DateTime dt)
        {
            if (ModelState.IsValid)
            {
                var drivers = await _driversService.GetByDateHigherAndLower(dt);
                return Ok(drivers);
            }
            else return BadRequest();
        }

        [Route("drivers")]
        [HttpPost]
        public async Task<IHttpActionResult> Add([FromBody] DriverBLL model)
        {
            if (ModelState.IsValid)
            {
                await _driversService.Add(model);
                return StatusCode(HttpStatusCode.NoContent);
            }
            else return BadRequest();
        }
    }
}
