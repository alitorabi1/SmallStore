using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmallStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallStore.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void Transaction_OrderSubmitTest()
        {
            try
            {
                Database db = new Database();
                OrderSummary or = new OrderSummary();
                db.Transaction_OrderSubmit(or, 1, 1);
                Assert.Fail();
            }
            catch(Exception e)
            {

            }
        }
    }
}