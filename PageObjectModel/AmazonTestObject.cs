using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjectModel.Pages;

namespace PageObjectModel
{
    // todo: saparate filter, add get and set to the homepage, add prperties to classes, add singleton to searchbar
    class AmazonTestObject
    {
        private IWebDriver driver;
        public PageObjectModel.Pages.HomePage Home;
        public PageObjectModel.Pages.ResultsPage Results;

        public AmazonTestObject(IWebDriver driver)
        {
            this.driver = driver;
            Home = new HomePage(driver);
            Results = new ResultsPage(driver);
        }

        public void Start()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void Close()
        {
            driver.Close();
        }
    }
}
