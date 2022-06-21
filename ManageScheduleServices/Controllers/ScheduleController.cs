using CommonServices.Model;
using ManageScheduleServices.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageScheduleServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class ScheduleController : ControllerBase
    {
        private IScheduleRepository schedule;
        public ScheduleController(IScheduleRepository _schedule)
        {
            schedule = _schedule;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Schvalue1", "Schvalue2" };
        }


        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "admin")]
        public Schedule AddSchedule([FromBody]Schedule sch)
        {
            return schedule.Add(sch);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "admin")]
        public Schedule Delete(int id)
        {
            return schedule.Delete(id);
        }

        [HttpGet]
        [Route("getAll")]
        [Authorize(Roles = "admin")]
        public IQueryable GetAllSchedules()
        
        {
            try
            {
                return schedule.GetAllSchedules();

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("search")]
        [Authorize(Roles = "user")]
        public IQueryable GetSchedule(string from, string to)
        {
            return schedule.GetScheduleBasedOnPlaces(from, to);
        }

        [HttpPost]
        [Route("update")]
        [Authorize(Roles = "admin")]
        public Schedule UpdateSchedule([FromBody]Schedule schchange)
        {
            return schedule.UpdateSchedule(schchange);
        }
    }
}
