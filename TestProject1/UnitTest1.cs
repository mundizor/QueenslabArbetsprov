using NUnit.Framework;
using System;
using System.Linq;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var pricecalc = new Arbetsprov.Controllers.PricingCalculatorController(@"..\..\..\..\");
            // Customer id = 1, CustomerX
            float price = pricecalc.GetCustomerCost(1, new DateTime(2019,09,20), new DateTime(2019, 10, 01));
            
            Assert.LessOrEqual(6.16, price);
            Assert.GreaterOrEqual(6.17, price);
        }        
        
        [Test]
        public void Test2()
        {

            var pricecalc = new Arbetsprov.Controllers.PricingCalculatorController(@"..\..\..\..\");
            // Customer id = 2, CustomerY
            float price = pricecalc.GetCustomerCost(2, new DateTime(2018, 01, 01), new DateTime(2019, 10, 01));

            Assert.LessOrEqual(175.4, price);
            Assert.GreaterOrEqual(175.6, price);
        }
    }
}