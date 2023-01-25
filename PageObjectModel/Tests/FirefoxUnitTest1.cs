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
            dictConditions.Add("Price_Lower_Then", "100");
            dictConditions.Add("Price_Hiegher_OR_Equal_Then", "50");
            dictConditions.Add("Free_Shipping", "true");

            BrowserFactory.LoadApplication("https://www.amazon.com/", firefoxDriver);
            Amazon.Start();
            Amazon.Pages.Home.SearchBar.Text = "mouse"; 
            Amazon.Pages.Home.SearchBar.Click();
            List<Item> items = Amazon.Pages.Results.GetResultsBy(dictConditions);
            Amazon.Pages.Results.PrintItems(items);
        }

        [TearDown]
        public void Close()
        {
            BrowserFactory.CloseDriver(firefoxDriver);
        }
    }
}