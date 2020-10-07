using IBA_Test_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace IBA_Test.Controllers
{
    public class DriversController : ApiController
    {
        [Route("drivers/datespeed")]
        [HttpPost]
        public IHttpActionResult GetByDateAndSpeed([FromBody] DriverFilterViewModel model)
        {
            if (ModelState.IsValid)
                return Ok(new List<DriverBLL>() { new DriverBLL { DateTime = model.DateTime, CarNumber = "1232 AP-7", Speed = 99.9f } });
            else return BadRequest();
        }

        [Route("drivers/date")]
        [HttpPost]
        public IHttpActionResult GetByDate([FromBody] DriverFilterViewModel model)
        {
            if (ModelState.IsValid)
                return Ok(new List<DriverBLL>() { new DriverBLL { DateTime = model.DateTime, CarNumber = "1232 AP-7", Speed = 99.9f } });
            else return BadRequest();
        }

        [Route("drivers")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] DriverBLL model)
        {
            if (ModelState.IsValid)
                return StatusCode(HttpStatusCode.NoContent);
            else return BadRequest();
        }
    }
}
