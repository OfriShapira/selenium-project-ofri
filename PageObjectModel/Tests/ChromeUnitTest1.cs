using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjectModel.Components;
using System.Collections;

namespace PageObjectModel.Tests
{
    public class ChromeUnitTest1
    {
        private IWebDriver chromeDriver;
        private Amazon Amazon;


        [SetUp]
        public void Setup()
        {
            chromeDriver = BrowserFactory.GetDriver("chrome", new List<string> {});
            Amazon = new Amazon(chromeDriver);
        }

        [Test]
        public void MouseResultsTest()
        {
            Dictionary<string, string> dictConditions = new Dictionary<string, string>();
            BrowserFactory.LoadApplication("https://www.amazon.com/", chromeDriver);
            dictConditions.Add("Price_Lower_Then", "100");
            dictConditions.Add("Price_Hiegher_OR_Equal_Then", "50");
            dictConditions.Add("Free_Shipping", "true");
            
            Amazon.Start();
            Amazon.Pages.Home.SearchBar.Text = "mouse";
            Amazon.Pages.Home.SearchBar.Click();
            List<Item> items = Amazon.Pages.Results.GetResultsBy(dictConditions);
            Amazon.Pages.Results.PrintItems(items);
           
            Assert.That(items.Count, Is.EqualTo(0));

        }

        [Test]
        // Another test method, to test a scenario which actully retrive results
        public void MonitorResultsTest()
        {
            Dictionary<string, string> dictConditions = new Dictionary<string, string>();
            BrowserFactory.LoadApplication("https://www.amazon.com/", chromeDriver);
            dictConditions.Add("Price_Lower_Then", "100");
            dictConditions.Add("Price_Hiegher_OR_Equal_Then", "50");
            dictConditions.Add("Free_Shipping", "false");
            
            Amazon.Start();
            Amazon.Pages.Home.SearchBar.Text = "Computer Monitor";
            Amazon.Pages.Home.SearchBar.Click();
            List<Item> items = Amazon.Pages.Results.GetResultsBy(dictConditions);
            Amazon.Pages.Results.PrintItems(items);
            
            Assert.That(items.Count, Is.EqualTo(3));
        }

        [TearDown]
        public void Close()
        {
            BrowserFactory.CloseDriver(chromeDriver);
        }
    }
}