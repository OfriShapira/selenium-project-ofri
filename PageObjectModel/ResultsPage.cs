using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PageObjectModel
{
    class ResultsPage
    {
        private IWebDriver driver;
        public SearchBar SearchBar;
        public Header Header;
        public Filters Filters;
        public List<ResultsItem> ResultsItems;
        
        public ResultsPage(IWebDriver webDriver) 
        {
            driver = webDriver;
            SearchBar = new SearchBar(driver);
            Header = new Header();
            Filters = new Filters();
            ResultsItems = new List<ResultsItem>();
        }
        
        public void GetAllItems()
        {
            IWebElement ulElement = driver.FindElement(By.TagName("ul"));
            IList<IWebElement> liElements = ulElement.FindElements(By.TagName("li"));

            // Start from index 1 so we skip the irelevant tag (categories)
            foreach(IWebElement liElement in liElements)
            {
                Console.WriteLine(liElement);
            }
        }


    }
}
