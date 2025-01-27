﻿using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Quartz
{
    public class QuartzJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobKey = JobKey.Create(nameof(QuartzJob));
            options.AddJob<QuartzJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            .AddTrigger(trigger =>
                        trigger.ForJob(jobKey)
                               .WithSimpleSchedule(schedule =>
                                    schedule.WithIntervalInSeconds(5).RepeatForever()));
        }
    }
}
