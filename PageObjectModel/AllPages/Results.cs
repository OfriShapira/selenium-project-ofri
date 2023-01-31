using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using PageObjectModel.Components;
using System.Diagnostics;
using NUnit.Framework.Constraints;

namespace PageObjectModel.AllPages
{
    class Results
    {
        private IWebDriver driver;
        private SearchBar searchBar;
        
        public Results(IWebDriver webDriver) 
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

        // Method which gets a dictionary of conditions and returns a list of items accodrding to those conditions
        public List<Item> GetResultsBy(Dictionary<string, string> conditions)
        {
            List<Item> results = new List<Item>();
            
            string mainXpath = "//span[@class='a-price' ";
            string closingXpath = "]//ancestor::div[@class='a-section a-spacing-small a-spacing-top-small']";

            if (conditions.ContainsKey("Price_Lower_Then"))
            {
                // extract the price value from the span, and check if the it is lower than the input
                mainXpath += $" and number(substring-before(substring((.), 2, 30), '$')) < {conditions["Price_Lower_Then"]}";
            }

            if (conditions.ContainsKey("Price_Hiegher_OR_Equal_Then"))
            {
                // extract the price value from the span, and check if it is higher or equal than the input
                mainXpath += $" and number(substring-before(substring((.), 2, 30), '$')) >= {conditions["Price_Hiegher_OR_Equal_Then"]} ";
            }

            if (conditions.ContainsKey("Free_Shipping"))
            {
                string isFreeShipped = conditions["Free_Shipping"];
                if (isFreeShipped == "true")
                {
                    // check if the ancestor div of the span contains span with 'free shipping' text 
                    mainXpath += " and .//ancestor::div[@class='sg-row' and .//span[contains(text(), 'FREE Shipping')]] ";
                }
                else if (isFreeShipped == "false")
                {
                    // check if the ancestor div of the span does not contains span with 'free shipping' text 
                    mainXpath += " and .//ancestor::div[@class='sg-row' and .//span[not(contains(text(), 'FREE Shipping'))]] ";
                }
            }

            // concat the main xpath to the xpath string which points to the parent div of each product
            mainXpath += closingXpath;

            IList<IWebElement> itemsList = driver.FindElements(By.XPath(mainXpath));

            // adds the products we found from the xpath expression, if their conditions are fulfilled, to the items list
            foreach (IWebElement item in itemsList)
            {
                IWebElement linkElement = item.FindElement(By.XPath(".//a[@class='a-size-base a-link-normal s-underline-text s-underline-link-text s-link-style a-text-normal']"));
               
                // find each field of the Item object, by search the xpath on each product item we found from the main xpath
                string link = linkElement.GetAttribute("href");
                string currentTitle = item.FindElement(By.XPath(".//span[@class='a-size-medium a-color-base a-text-normal']")).Text;
                string priceWhole = item.FindElement(By.XPath(".//span[@class='a-price-whole']")).Text;
                string priceFraction = item.FindElement(By.XPath(".//span[@class='a-price-fraction']")).Text;
                string priceFull = '$' + priceWhole + '.' + priceFraction;
                
                // finally, insert the fields into a new Item
                results.Add(new Item(currentTitle, priceFull, link));
            }

            return results;
        }

        // Prints all the items in the list
        public void PrintItems(List<Item> items)
        {
            for(int i = 0; i < items.Count; i++) 
            {
                // if this is the last item in the list, do not insert a comma between the prints
                if (i != items.Count - 1)
                {
                   Console.WriteLine(items[i].ToString() + ",");
                }
                else
                {
                    Console.WriteLine(items[i].ToString());
                }
               
            }
        }
    }
}