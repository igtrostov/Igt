using Microsoft.VisualStudio.TestTools.UnitTesting;
using IgtTask;

namespace UnitTestIgt
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCorrectCase()
        {
            string input = "2017-05-23 15:38:40.0069|Info|HandleRailOCREvents|Handle|Request for 37058_2234_1_GetImages";
            string delimiter = ",";
            string output = "2017-05-23,15:38:40.0069,37058,2234,1,GetImages";

            bool res = InputConverter.ConvertInput(input, delimiter, out string csv_line);

            Assert.AreEqual(true, res);
            Assert.AreEqual(output, csv_line);
        }

        [TestMethod]
        public void TestIncorrectCase()
        {
            string input = "2017-05-23 12:19:13.8206|Info||Main|Starting P2P service";
            string delimiter = ",";
            string output = "";

            bool res = InputConverter.ConvertInput(input, delimiter, out string csv_line);

            Assert.AreEqual(false, res);
            Assert.AreEqual(output, csv_line);
        }
    }
}