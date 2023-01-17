using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjectModel.Components;

namespace PageObjectModel.Tests
{
    public class ChromeUnitTest1
    {
        private IWebDriver chromeDriver;
        private EbayTestObject chromeTester;


        [SetUp]
        public void Setup()
        {
            chromeDriver = BrowserFactory.GetDriver("chrome", new List<string> {});
            chromeTester = new EbayTestObject(chromeDriver);
        }

        /*        [Test]*/
        /*        public void ChromeTest()
                {
                    *//*
                     Get All Prices Larger Than In Chrome
                    *//*
                   BrowserFactory.LoadApplication("https://www.ebay.com/", chromeDriver);
                   chromeTester.Start();
                   chromeTester.Home.SearchBar.SearchFor("Mouse");
                   IList<double> prices = chromeTester.Results.GetAllPricesHigherThan(50);
                   foreach (double price in prices)
                   {
                        Console.WriteLine(price);
                   }
                }*/
        /*        [Test]
                public void ChromeTestWithFilter() {
                    BrowserFactory.LoadApplication("https://www.ebay.com/", chromeDriver);
                    chromeTester.Start();
                    chromeTester.Home.SearchBar.SearchFor("Mouse");
                    chromeTester.Results.FilterBy(50);
                }*/
        [Test]
        public void GetItemsTest()
        {
            BrowserFactory.LoadApplication("https://www.ebay.com/", chromeDriver);
            chromeTester.Start();
            chromeTester.Home.SearchBar.SearchFor("Mouse");
            chromeTester.Results.GetItemList();
        }



        [TearDown]
        public void Close()
        {
           BrowserFactory.CloseDriver(chromeDriver);
        }
    }
}