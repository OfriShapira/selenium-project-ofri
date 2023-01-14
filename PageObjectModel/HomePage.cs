using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModel
{
    class HomePage
    {
        private IWebDriver driver;
        public SearchBar SearchBar;
        public Header Header;
        public Categories Categories = new Categories();

        public HomePage(IWebDriver webDriver)
        {
            driver = webDriver;
            SearchBar = new SearchBar(driver);
            Header = new Header();
            Categories = new Categories();
        }

    }
}
