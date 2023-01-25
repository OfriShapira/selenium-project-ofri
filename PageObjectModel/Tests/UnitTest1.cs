using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjectModel.Components;
using System.Collections;

namespace PageObjectModel.Tests
{
    public class UnitTest1
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
        public void FirstTest()
        {
            Dictionary<string, string> dictConditions = new Dictionary<string, string>();
            BrowserFactory.LoadApplication("https://www.amazon.com/", chromeDriver);
            dictConditions.Add("Price_Lower_Then", "100");
            dictConditions.Add("Price_Hiegher_OR_Equal_Then", "50");
            dictConditions.Add("Free_Shipping", "true");
            Amazon.Start();
            Amazon.Pages.Home.SearchBar.Text = "mouse";
            Amazon.Pages.Home.SearchBar.Click();
            Amazon.Pages.Results.GetResultsBy(dictConditions);
        } 

        [TearDown]
        public void Close()
        {
           /* BrowserFactory.CloseDriver(chromeDriver);*/
        }
    }
}