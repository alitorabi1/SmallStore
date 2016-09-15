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
    public class TotalPricePerItemTests
    {
        [TestMethod()]
        public void ConvertTestFORTotalPricePerItem()
        {
            OrderItem orItem = new OrderItem();
            orItem.SalePricePerUnit = 100;
            orItem.NumberOfUnit = 5;
            TotalPricePerItem tP = new TotalPricePerItem();
            object o = orItem;
            CultureInfo c = new CultureInfo("");
           var r= tP.Convert(orItem, typeof(Decimal), o,c);
            decimal value = Convert.ToDecimal(r);
            Assert.AreEqual(value, 500);
        }
    }
}