using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PageObjectModel.Tests
{
    public class FirefoxUnitTest1
    {
        private IWebDriver firefoxDriver;
        private AmazonTestObject ebayTester;

        [SetUp]
        public void Setup()
        {
            firefoxDriver = BrowserFactory.GetDriver("firefox", new List<string> { });
            ebayTester = new AmazonTestObject(firefoxDriver);
        }

        [Test]
        public void FireFoxTest()
        {
            /*
             Get All Prices Larger Than In Firefox
            */
            BrowserFactory.LoadApplication("https://www.ebay.com/", firefoxDriver);
            ebayTester.Start();
            ebayTester.Home.SearchBar.SearchFor("Keyboard");
            IList<double> prices = ebayTester.Results.GetAllPricesHigherThan(0);
            foreach (double price in prices)
            {
                Console.WriteLine(price);
            }
        }

        [TearDown]
        public void Close()
        {
            BrowserFactory.CloseDriver(firefoxDriver);
        }
    }
}