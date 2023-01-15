using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PageObjectModel.Tests
{
    public class ChromeUnitTest1
    {
        private IWebDriver chromeDriver;
        private EbayTestObject chromeTester;


        [SetUp]
        public void Setup()
        {
            chromeDriver =  BrowserFactory.GetDriver("chrome", new List<string> { "--headless"});
            chromeTester = new EbayTestObject(chromeDriver);
        }

        [Test]
        public void ChromeTest()
        {
            /*
             Get All Prices Larger Than In Chrome
            */
           BrowserFactory.LoadApplication("https://www.ebay.com/", chromeDriver);
           chromeTester.Start();
           chromeTester.Home.SearchBar.SearchFor("Mouse");
           IList<double> prices = chromeTester.Results.GetAllPricesHigherThan(50);
           foreach (double price in prices)
           {
                Console.WriteLine(price);
           }
        }

        [TearDown]
        public void Close()
        {
            BrowserFactory.CloseDriver(chromeDriver);
        }
    }
}