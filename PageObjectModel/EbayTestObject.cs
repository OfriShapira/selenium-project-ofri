using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjectModel.Pages;

namespace PageObjectModel
{
    class EbayTestObject
    {
        private IWebDriver driver;
        public HomePage Home;
        public ResultsPage Results;
        public EbayTestObject(string brwoser, ChromeOptions options) {
            driver  = new ChromeDriver(@".\Drivers", options);
            Home = new HomePage(driver);
            Results = new ResultsPage(driver);
        }

        public void Start()
        {
            driver.Url = "https://www.ebay.com/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void Close()
        {
            driver.Quit();
        }
    }
}
