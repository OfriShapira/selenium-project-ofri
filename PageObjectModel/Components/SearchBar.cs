using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModel.Components
{
    class SearchBar
    {
        private IWebElement searchBar, searchIcon;
        private IWebDriver driver;
        public SearchBar(IWebDriver webDriver)
        { 
            driver = webDriver;
        }

        public void SearchFor(string search)
        {

            try
            {
                searchBar = driver.FindElement(By.Id("gh-ac"));
            }
            catch(Exception e)
            {
                Console.WriteLine($"Driver didn't find the search bar component. The exception is: {e}");
            }

            try
            {
                searchIcon = driver.FindElement(By.Id("gh-btn"));
            }
            catch(Exception e)
            {
                Console.WriteLine($"Driver didn't find the search button. The exception is: {e}");
            }

            searchBar.SendKeys(search);
            searchIcon.Click();
        }
    }
}
