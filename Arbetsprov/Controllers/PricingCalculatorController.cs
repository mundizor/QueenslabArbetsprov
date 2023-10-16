using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arbetsprov.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PricingCalculatorController : ControllerBase
    {
        private class ServiceDefaultPrices
        {
            //days are set in array with sunday first and saturday last. null is a non-paying day. 
            public static Dictionary<Service, List<float?>> priceList = new Dictionary<Service, List<float?>>() {
                {Service.ServiceA, new List<float?>() { null, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, null } },
                {Service.ServiceB, new List<float?>() { null, 0.24f, 0.24f, 0.24f, 0.24f, 0.24f, null } },
                {Service.ServiceC, new List<float?>() {0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f } },
            
            };
        }

        [HttpGet]
        public float GetCustomerCost(int CustomerId, DateTime startDate, DateTime EndDate)
        {
            using var db = new PriceCheckContext();
            float costSum = 0;

            var customer = db.Users.Include(x => x.ActiveServicesTimes).Include(x=>x.DiscountDaysPeriods).Include(x => x.FreeDaysPeriods).Include(x => x.PriceOverrides)
                .Where(x => x.UserId == CustomerId)
                .FirstOrDefault();

            //for every service available we check if the user has any active period in that service.
            foreach (Service service in Enum.GetValues(typeof(Service)))
            {
                var activeservices = customer.ActiveServicesTimes.Where(x => x.service == service);
                if (activeservices.Count() == 0)
                    continue; //this user has never had any active subscription of this service.

                var userDiscounts = customer.DiscountDaysPeriods.Where(x => x.Service == service);

                var userFreeDays = customer.FreeDaysPeriods;

                //Go through every day between input dates.
                var ItrDay = startDate;
                while (ItrDay <= EndDate)
                {
                    if(IsDayFree(ItrDay, userFreeDays))
                    {
                        ItrDay = ItrDay.AddDays(1);
                        continue; //this day is free.
                    }

                    if(IsDayActive(ItrDay, activeservices) == false)
                    {
                        ItrDay = ItrDay.AddDays(1);
                        continue; //this day does not have an active subscription.
                    }

                    //gets price. Default or override price.
                    float dayprice = GetPriceForService(ItrDay, service, customer.PriceOverrides);

                    //apply discount if available
                    dayprice = ApplyDiscount(dayprice, ItrDay, userDiscounts);


                    costSum += dayprice;
                    ItrDay = ItrDay.AddDays(1);
                }
            }

            return costSum;
        }

        private float ApplyDiscount(float price,DateTime day,  IEnumerable<DiscountDaysPeriod> discounts)
        {
            foreach (var item in discounts)
            {
                if(day >= item.StartDate && day <= item.EndDate)
                {
                    price *= (1.0f - ((float)item.DiscountPercentage / 100));
                }
            }

            return price;
        }

        private float GetPriceForService(DateTime Day, Service service, IEnumerable<PriceOverride> priceOverrides)
        {
            if (ServiceDefaultPrices.priceList[service][(int)Day.DayOfWeek] == null)
                return 0; //This is a non-pay day.

            if (priceOverrides.Count() > 0)
            {
                //apply price override

                var priceOverride = priceOverrides.FirstOrDefault();
                if (service == Service.ServiceA)
                    return priceOverride.ServiceAPrice.HasValue ? priceOverride.ServiceAPrice.Value : ServiceDefaultPrices.priceList[service][((int)Day.DayOfWeek)].Value;
                if (service == Service.ServiceB)
                    return priceOverride.ServiceBPrice.HasValue ? priceOverride.ServiceBPrice.Value : ServiceDefaultPrices.priceList[service][((int)Day.DayOfWeek)].Value;
                if (service == Service.ServiceC)
                    return priceOverride.ServiceCPrice.HasValue ? priceOverride.ServiceCPrice.Value : ServiceDefaultPrices.priceList[service][((int)Day.DayOfWeek)].Value;


                return 0; //default return. should never occur.
            }

            //apply default price
            return ServiceDefaultPrices.priceList[service][(int)Day.DayOfWeek].Value;
        }

        private bool IsDayFree(DateTime day, ICollection<FreeDaysPeriod> freedays)
        {
            foreach (var freedayPeriod in freedays)
            {
                if (day >= freedayPeriod.StartDate && day <= freedayPeriod.EndDate)
                    return true;
            }

            return false;
        }        
        
        private bool IsDayActive(DateTime day, IEnumerable<ActiveServicesTime> activePeriods)
        {
            foreach (var activePeriod in activePeriods)
            {
                if(activePeriod.EndDate == null)
                {
                    if (day >= activePeriod.StartDate)
                        return true;
                }
                else 
                {
                    if (day >= activePeriod.StartDate && day <= activePeriod.EndDate.Value)
                        return true;
                }


            }

            return false;
        }
    }
}
