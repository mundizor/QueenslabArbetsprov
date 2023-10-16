using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class PriceCheckContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public string DbPath { get; }

    public PriceCheckContext(string dbpath_)
    {
        //var folder = Environment.SpecialFolder.LocalApplicationData;
        //var path = Environment.GetFolderPath(folder);
        //DbPath = System.IO.Path.Join(path, "pricecheck.db");
        DbPath = System.IO.Path.GetFullPath(dbpath_) + "pricecheck.db"; //hacky solution to get db path. should have initialized data in ef instead.
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

//commands after updating models for db.
//dotnet ef migrations add AddBlogCreatedTimestamp
//dotnet ef database update


//db classes placed here temporarly. would not be put here in a real project.
public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }

    public ICollection<DiscountDaysPeriod> DiscountDaysPeriods { get; } = new List<DiscountDaysPeriod>();
    public ICollection<PriceOverride> PriceOverrides { get; } = new List<PriceOverride>();
    public ICollection<ActiveServicesTime> ActiveServicesTimes { get; } = new List<ActiveServicesTime>();
    public ICollection<FreeDaysPeriod> FreeDaysPeriods { get; } = new List<FreeDaysPeriod>();

}

public class DiscountDaysPeriod
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int DiscountPercentage { get; set; }
    public Service Service { get; set; }

    public User User { get; set; }

}
public class FreeDaysPeriod
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public User User { get; set; }

}

public class PriceOverride
{
    public int Id { get; set; }
    public float? ServiceAPrice { get; set; }
    public float? ServiceBPrice { get; set; }
    public float? ServiceCPrice { get; set; }

    public User User { get; set; }

}

public class ActiveServicesTime
{
    public int Id { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Service service { get; set; }


    public User User { get; set; }
}

public enum Service
{
    ServiceA,
    ServiceB,
    ServiceC
}