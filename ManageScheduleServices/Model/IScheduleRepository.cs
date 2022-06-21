using CommonServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageScheduleServices.Model
{
    public interface IScheduleRepository
    {
        Schedule Add(Schedule sch);
        Schedule Delete(int id);
        IQueryable GetAllSchedules();

        IQueryable GetScheduleBasedOnPlaces(string from, string to);

        Schedule UpdateSchedule(Schedule schchange);
    }
}
