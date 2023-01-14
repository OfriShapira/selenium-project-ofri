using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModel
{
    class EbayTestObject
    {
        private IWebDriver driver;
        public HomePage Home;
        public ResultsPage Results;
        public EbayTestObject(IWebDriver webDriver) {
            driver  = new ChromeDriver(@"C:\Users\ofris\source\drivers");
            Home = new HomePage(driver);
            Results = new ResultsPage(driver);
        }

        public void Start()
        {
            var optoins = new ChromeOptions();
            optoins.AddArgument("--headless");
            driver.Url = "https://www.ebay.com/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
        }

        public void Close()
        {
            driver.Close();
        }
    }
}
