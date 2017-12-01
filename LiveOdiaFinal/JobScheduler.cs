using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveOdiaFinal
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<autoUpdate>().Build();
            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithDailyTimeIntervalSchedule(
            //    s =>s.WithIntervalInHours(1).
            //    OnEveryDay().
            //    StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0)))
            //    .Build();
            var runAt = DateTime.Now.Date + TimeSpan.FromMinutes(1);
            ///var runAt =  DateTime.Now.Date;
            ITrigger trigger = TriggerBuilder.Create()
                .StartAt(runAt)
                .Build();
            scheduler.ScheduleJob(job, trigger);
        }
    }
}