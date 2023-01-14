using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModel
{
    class SearchBar
    {
        private IWebDriver driver;
        private IWebElement searchBar, searchIcon;
        public SearchBar(IWebDriver webDriver)
        { 
            driver = webDriver;
        }

        public void SearchFor(string search)
        {
            searchBar = driver.FindElement(By.Id("gh-ac"));
            searchIcon = driver.FindElement(By.Id("gh-btn"));
            searchBar.SendKeys(search);
            searchIcon.Click();
        }
    }
}
