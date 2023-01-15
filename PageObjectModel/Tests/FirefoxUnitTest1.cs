using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PageObjectModel.Tests
{
    public class FirefoxUnitTest1
    {
        private IWebDriver firefoxDriver;
        private EbayTestObject fireFoxTester;

        [SetUp]
        public void Setup()
        {
            firefoxDriver = BrowserFactory.GetDriver("firefox", new List<string> { });
            fireFoxTester = new EbayTestObject(firefoxDriver);
        }

        [Test]
        public void FireFoxTest()
        {
            /*
             Get All Prices Larger Than In Firefox
            */
            BrowserFactory.LoadApplication("https://www.ebay.com/", firefoxDriver);
            fireFoxTester.Start();
            fireFoxTester.Home.SearchBar.SearchFor("Keyboard");
            IList<double> prices = fireFoxTester.Results.GetAllPricesHigherThan(0);
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