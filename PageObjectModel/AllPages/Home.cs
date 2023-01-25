using OpenQA.Selenium;
using PageObjectModel.Components;

namespace PageObjectModel.AllPages
{
    class Home
    {
        private IWebDriver driver;
        private SearchBar searchBar;

        public Home(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public SearchBar SearchBar
        {
            get
            {
                if (searchBar == null)
                {
                    searchBar = new SearchBar(driver);
                }
                return searchBar;
            }
        }

    }
}
