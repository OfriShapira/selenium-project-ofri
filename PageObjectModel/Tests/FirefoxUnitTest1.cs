using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjectModel.Components;

namespace PageObjectModel.Tests
{
    public class FirefoxUnitTest1
    {
        private IWebDriver firefoxDriver;
        private Amazon Amazon;

        [SetUp]
        public void Setup()
        {
            firefoxDriver = BrowserFactory.GetDriver("firefox", new List<string> { });
            Amazon  = new Amazon(firefoxDriver);
        }

        [Test]
        public void FireFoxTest()
        {
            Dictionary<string, string> dictConditions = new Dictionary<string, string>();
            BrowserFactory.LoadApplication("https://www.amazon.com/", firefoxDriver);
            dictConditions.Add("Price_Lower_Then", "1000");
            dictConditions.Add("Price_Hiegher_OR_Equal_Then", "0");
            dictConditions.Add("Free_Shipping", "true");
            Amazon.Start();
            Amazon.Pages.Home.SearchBar.Text = "mouse"; // return 0 results 
            // the following assginment returns more than 0 results, with the current conditions
            //Amazon.Pages.Home.SearchBar.Text = "computer monitor"; 
            Amazon.Pages.Home.SearchBar.Click();
            List<Item> items = Amazon.Pages.Results.GetResultsBy(dictConditions);
            Amazon.Pages.Results.PrintItems(items);
            /* Assert.Equals(1, items.Count);*/
        }

        [TearDown]
        public void Close()
        {
            BrowserFactory.CloseDriver(firefoxDriver);
        }
    }
}