using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjectModel.Components;
using System.Collections;
using System.Reflection.Metadata;

namespace PageObjectModel.Tests
{
    public class UnitTest1
    {
        private IWebDriver driver;
        private Amazon Amazon;
        private string parameterToSearch = "mouse";
        private string browserToTest = "chrome";

        [SetUp]
        public void Setup()
        {
            // Create the browser driver instance with its optional options dictionary
            driver = BrowserFactory.GetDriver(browserToTest, new List<string> {});
            Amazon = new Amazon(driver);
        }

        [Test]
        public void MouseResultsTest()
        {
            Dictionary<string, string> dictConditions = new Dictionary<string, string>();
            BrowserFactory.LoadApplication("https://www.amazon.com/", driver);
            
            Amazon.Start();
            Amazon.Pages.Home.SearchBar.Text = parameterToSearch;
            Amazon.Pages.Home.SearchBar.Click();
            List<Item> items = Amazon.Pages.Results.GetResultsBy(new Dictionary<string, string>()
            {
                {"Price_Lower_Then","100"},
                {"Price_Hiegher_OR_Equal_Then","50"},
                {"Free_Shipping","true"}
            });
            Amazon.Pages.Results.PrintItems(items);

            Assert.That(items, Is.Not.EqualTo(null));
        }

        [TearDown]
        public void Close()
        {
            BrowserFactory.CloseDriver(driver);
        }
    }
}