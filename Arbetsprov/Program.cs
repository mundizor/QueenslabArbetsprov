using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arbetsprov
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();

        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        //private static void AddDatabaseData()
        //{
        //Code used to input correct data into database.

        //using var db = new PriceCheckContext();
        //System.Diagnostics.Debug.WriteLine($"Database path: {db.DbPath}.");

        // Create
        //db.Add(new User { Name = "CustomerX" });
        //db.Add(new User { Name = "CustomerY" });
        //db.Add(new DiscountDaysPeriod { StartDate = new DateTime(2019, 09,22), EndDate = new DateTime(2019, 09, 24), DiscountPercentage = 20, Service = Service.ServiceC });
        //db.Add(new ActiveServicesTime { StartDate = new DateTime(2019, 09,20), EndDate = null, service = Service.ServiceA });
        //db.Add(new ActiveServicesTime { StartDate = new DateTime(2019, 09,20), EndDate = null, service = Service.ServiceC });
        //db.SaveChanges();

        /*var customerX = db.Users
            .Where(x => x.Name == "CustomerX")
            .First();

        customerX.DiscountDaysPeriods.Add(new DiscountDaysPeriod { StartDate = new DateTime(2019, 09, 22), EndDate = new DateTime(2019, 09, 24), DiscountPercentage = 20, Service = Service.ServiceC });
        customerX.ActiveServicesTimes.Add(new ActiveServicesTime { StartDate = new DateTime(2019, 09, 20), EndDate = null, service = Service.ServiceA });
        customerX.ActiveServicesTimes.Add(new ActiveServicesTime { StartDate = new DateTime(2019, 09, 20), EndDate = null, service = Service.ServiceC });
        db.SaveChanges();*/

        /*var customerY = db.Users
            .Where(x => x.Name == "CustomerY")
            .First();

        DateTime startdate = new DateTime(2018, 01, 01);

        customerY.FreeDaysPeriods.Add(new FreeDaysPeriod { StartDate = startdate, EndDate = startdate.AddDays(199) });
        customerY.DiscountDaysPeriods.Add(new DiscountDaysPeriod { StartDate = startdate.AddDays(200), EndDate = new DateTime(2019, 10, 01), DiscountPercentage = 30, Service = Service.ServiceB });
        customerY.DiscountDaysPeriods.Add(new DiscountDaysPeriod { StartDate = startdate.AddDays(200), EndDate = new DateTime(2019, 10, 01), DiscountPercentage = 30, Service = Service.ServiceC });
        customerY.ActiveServicesTimes.Add(new ActiveServicesTime { StartDate = startdate, EndDate = null, service = Service.ServiceB });
        customerY.ActiveServicesTimes.Add(new ActiveServicesTime { StartDate = startdate, EndDate = null, service = Service.ServiceC });
        db.SaveChanges();*/

        // Update
        //Console.WriteLine("Updating the blog and adding a post");
        //blog.Url = "https://devblogs.microsoft.com/dotnet";
        //blog.Posts.Add(
        //    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
        //db.SaveChanges();

        // Delete
        //Console.WriteLine("Delete the blog");
        //db.Remove(blog);
        //db.SaveChanges();
        //}
    }
}
