using IBA_Test.Filters;
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
        /// <summary>
        /// Получение выборки данных за определенную дату, со скоростью, превышающей заданную
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AccessAction]
        [Route("drivers/datespeed")]
        [HttpPost]
        public async Task<IHttpActionResult> GetByDateAndSpeed([FromBody] DriverFilterDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _driversService.GetByDateAndSpeed(model.DateTime, model.Speed);
                return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);
            }
            else return BadRequest(ModelState);
        }

        /// <summary>
        /// Получение выборки данных с максимальной и минимальной скоростью за определенную дату
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AccessAction]
        [Route("drivers/date")]
        [HttpPost]
        public async Task<IHttpActionResult> GetByDate([FromBody] DriverMinMaxFilterDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _driversService.GetByDateHigherAndLower(model.dt);
                return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)BadRequest(result.Error);
            }
            else return BadRequest(ModelState);
        }
        /// <summary>
        /// Добавление информации 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("drivers")]
        [HttpPost]
        public async Task<IHttpActionResult> Add([FromBody] DriverDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _driversService.Add(model);
                return result.IsSuccess ? StatusCode(HttpStatusCode.NoContent) : (IHttpActionResult)BadRequest(result.Error);
            }
            else return BadRequest(ModelState);
        }
    }
}
