using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjectModel.AllPages;

namespace PageObjectModel
{
    class Amazon
    {
        private IWebDriver driver;
        private Pages pages;

        public Amazon(IWebDriver driver)
        {
            this.driver = driver;
        }

        public Pages Pages
        {
            get
            { 
                if (pages == null)
                {
                    pages = new Pages(driver);
                }
                return pages;
            }
        }

        // Method to start and setup the driver
        public void Start()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

    }
}
