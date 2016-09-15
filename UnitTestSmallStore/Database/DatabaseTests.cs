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
    public class DatabaseTests
    {
        [TestMethod()]
        public void LoginUserTest()
        {
            string user = "11";
            string pass = "22";
            bool isManager;
           Database db = new Database();
            var isLogined = db.LoginUser(user, pass, out isManager);

            
            Assert.AreEqual(isLogined, true);
          //  Assert.Fail();
            
        }
        [TestMethod()]
        public void LoginUserTestIsManager()
        {
            string user = "11";
            string pass = "22";
            bool isManager;
            Database db = new Database();
            var isLogined = db.LoginUser(user, pass, out isManager);


            Assert.AreEqual(isManager, false);
            //  Assert.Fail();

        }
        [TestMethod()]
        public void LoginUserTestFailed()
        {
            string user = "fff1";
            string pass = "2233";
            bool isManager;
            Database db = new Database();
            var isLogined = db.LoginUser(user, pass, out isManager);


           Assert.AreEqual(isLogined, false);
            //  Assert.Fail();

        }
    }
}