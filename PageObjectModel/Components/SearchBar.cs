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
        private IWebDriver driver;

        public SearchBar(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public string Text
        {
            get
            {
                // Find the search bar and get its text
                return driver.FindElement(By.Id("twotabsearchtextbox")).Text;
            }
            set
            {
                try
                {
                    // Send the text to the searchbar
                    driver.FindElement(By.Id("twotabsearchtextbox")).SendKeys(value); ;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Driver didn't find the search bar component. The exception is: {e}");
                }
            }
        }

        // Function to perform the click on the search icon
        public void Click()
        {
            driver.FindElement(By.Id("nav-search-submit-button")).Click();
        }
    }
}
