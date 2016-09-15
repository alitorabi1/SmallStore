using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SmallStore.Tests
{
    [TestClass()]
    public class DiscountProductConverterTests
    {
        [TestMethod()]
        public void DiscountCalculated()
        {
            Product p = new Product();
           // ((Product)value).SalePrice - ((Product)value).SalePrice * ((Product)value).SpecialDiscount;
           p.SalePrice = 100;
            p.SpecialDiscount = 0.25M;
            DiscountProductConverter dsc = new DiscountProductConverter();

          
            object o = dsc;
            CultureInfo c = new CultureInfo("");
            var r = dsc.Convert(p, typeof(Decimal), o, c);
            decimal resault = Convert.ToDecimal(r);
            decimal value = 75;
            Assert.AreEqual(value, resault);
          
        }
    }
}