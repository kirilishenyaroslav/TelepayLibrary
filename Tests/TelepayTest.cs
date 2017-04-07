using Microsoft.VisualStudio.TestTools.UnitTesting;
using TelepayLib;

namespace Tests
{
    [TestClass]
    public class TelepayTest
    {
        [TestMethod]
        public void TestParser()
        {
            var tele = new Telepay();
            string pathFor     = "../../../TelepayLib/Telepay foreign.txt";
            string pathDom     = "../../../TelepayLib/sample2013.TPY";
            string latest_file = "../../../TelepayLib/bankfile_from_oystein.TPY";

            string samDoc = tele.ParseDocument(latest_file);
            string forDoc = tele.ParseDocument(pathFor);
            string domDoc = tele.ParseDocument(pathDom);
            
        }

        [TestMethod]
        public void TestDomesticBatch()
        {
            var tele = new Telepay();
            var b1 = tele.createDomesticBatch();
            string s = b1.ToString();
        }

        [TestMethod]
        public void TestInternationalBatch()
        {
            var tele   = new Telepay();
            var b2     = tele.createInternationalBatch();
            string s1  = b2.ToString();
        }
    }
}