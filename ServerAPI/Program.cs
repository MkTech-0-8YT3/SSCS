using Common.DataModels.DatabaseModels;
using Common.IcsImporter;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence.Context;
using System;
using System.Threading.Tasks;

namespace ServerAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await ConfigureDatabase(host);
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        public static async Task ConfigureDatabase(IHost host)
        {
            //Testing purposes
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ISSCSDbContext>();
            var testDataLoader = new CalendarMapper();
            var classes = testDataLoader.ReadEventsFromCalendarFile(@"C:\code\temp\test.ics");
            //add test schedule to database
            var testSchedule = new Schedule()
            {
                Classes = classes,
                Department = Common.DataModels.Descriptors.Department.WBMiI,
                Major = Common.DataModels.Descriptors.Major.Inf,
                DegreeLevel = 1,
                Semester = 6,
                Group = "gr1",
                TypeOfStudies = Common.DataModels.Descriptors.TypeOfStudies.Extramural
            };
            try
            {
                await context.Database.EnsureCreatedAsync();
                await context.Schedules.AddAsync(testSchedule);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError("Error during db configuration: {0}", ex);
            }
        }
    }
}
