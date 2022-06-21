using CommonServices.Model;
using ManageAirlineServices.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageAirlineServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    [Authorize(Roles = "admin")]

    public class AirlineController : ControllerBase
    {
        private IAirlineRepository air;
        public AirlineController(IAirlineRepository _air)
        {
            air = _air;
        }
        // GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "Airvalue1", "Airvalue2" };
        //}

        
        [HttpPost]
        [Route("add")]
        public Airline AddAirline([FromBody]Airline airline)
        {
            return air.Add(airline);
        }

        [HttpDelete]
        [Route("delete")]
        public Airline DeleteAirline(int id)
        {
            return air.Delete(id);
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Airline> GetAllAirlines()
        {
            return air.GetAllAirlines();
        }

        [HttpGet]
        [Route("Get")]
        public Airline GetAirline(int id)
        {
            return air.GetAirline(id);
        }

        [HttpPost]
        [Route("Update")]
        public Airline UpdateAirline([FromBody]Airline a)
        {
            return air.UpdateAirline(a);
        }
    }
}
