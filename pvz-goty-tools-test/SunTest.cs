using Microsoft.VisualStudio.TestTools.UnitTesting;
using pvz_goty_tools;

namespace pvz_goty_tools_test
{
    [TestClass]
    public class SunTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Assert.AreEqual(Sun.GetSunValue(), 50);
            Assert.IsTrue(Sun.WriteSunValue(70));
            //Console.WriteLine(Sun.GetSunValue());
        }
    }
}
