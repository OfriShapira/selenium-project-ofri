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
        private BrowserFactory browserFactory;
        private string browserName;
        private List<string> browserOptions;

        public EbayTestObject(string browser, List<string> options)
        {
            browserName = browser;
            browserOptions = options;
            browserFactory = new BrowserFactory();
        }

        public void Start()
        {
            driver = browserFactory.InitBrowser(browserName, browserOptions);
            Home = new HomePage(driver);
            Results = new ResultsPage(driver);
            driver.Url = "https://www.ebay.com/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void Close()
        {
            driver.Close();
        }
    }
}
