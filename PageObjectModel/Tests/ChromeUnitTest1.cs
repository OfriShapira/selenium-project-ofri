using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjectModel.Components;

namespace PageObjectModel.Tests
{
    public class ChromeUnitTest1
    {
        private IWebDriver chromeDriver;
        private EbayTestObject ebayTester;


        [SetUp]
        public void Setup()
        {
            chromeDriver = BrowserFactory.GetDriver("chrome", new List<string> {});
            ebayTester = new EbayTestObject(chromeDriver);
        }

        [Test]
        public void ChromeTest()
        {
            BrowserFactory.LoadApplication("https://www.ebay.com/", chromeDriver);
            ebayTester.Start();
            ebayTester.Home.SearchBar.SearchFor("Mouse");
            IList<double> prices = ebayTester.Results.GetAllPricesHigherThan(50);
            foreach (double price in prices)
            {
                Console.WriteLine(price);
            }
        }
        /*        [Test]
                public void ChromeTestWithFilter()
                {
                    BrowserFactory.LoadApplication("https://www.ebay.com/", chromeDriver);
                    ebayTester.Start();
                    ebayTester.Home.SearchBar.SearchFor("Mouse");
                    ebayTester.Results.FilterBy(50);
                }*/
        /*        [Test]
                public void GetItemsTest()
                {
                    BrowserFactory.LoadApplication("https://www.ebay.com/", chromeDriver);
                    chromeTester.Start();
                    chromeTester.Home.SearchBar.SearchFor("Mouse");
                    chromeTester.Results.GetItemList();
                }*/

        /*        [Test]
                public void ChromeTestWithXpath()
                {
                    BrowserFactory.LoadApplication("https://www.ebay.com/", chromeDriver);
                    ebayTester.Start();
                    ebayTester.Home.SearchBar.SearchFor("Mouse");
                    ebayTester.Results.GetAllPricesHigherThanXPath(20);
                }*/

        [TearDown]
        public void Close()
        {
            BrowserFactory.CloseDriver(chromeDriver);
        }
    }
}