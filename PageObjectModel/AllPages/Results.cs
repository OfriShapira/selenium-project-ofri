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
        public List<Item> ResultsItems { get; set; }
        
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

        public List<Item> GetResultsBy(Dictionary<string, string> conditions)
        {
            List<Item> results = new List<Item>();

            string mainXpath = "(//span[@class='a-price' ";
            string closingXpath = "]//ancestor::div[@class='a-section a-spacing-small a-spacing-top-small'])";

            if (conditions.ContainsKey("Price_Lower_Then"))
            {
                mainXpath += $" and number(substring-before(substring((.), 2, 30), '$')) < {conditions["Price_Lower_Then"]}";
            }

            if (conditions.ContainsKey("Price_Hiegher_OR_Equal_Then"))
            {
                mainXpath += $" and number(substring-before(substring((.), 2, 30), '$')) >= {conditions["Price_Hiegher_OR_Equal_Then"]} ";
            }

            if (conditions.ContainsKey("Free_Shipping"))
            {
                string isFreeShipped = conditions["Free_Shipping"];
                if (isFreeShipped == "true")
                {
                    mainXpath += " and .//ancestor::div[@class='sg-row' and .//span[contains(text(), 'FREE Shipping')]] ";
                }
                else if (isFreeShipped == "false")
                {
                    mainXpath += " and .//ancestor::div[@class='sg-row' and .//span[not(contains(text(), 'FREE Shipping'))]] ";
                }
            }

            mainXpath += closingXpath;

            // Find the amount of items in the list
            IList<IWebElement> itemsList = driver.FindElements(By.XPath(mainXpath));

            // Add the matched results to the list
            for (int i = 0; i < itemsList.Count; i++)
            {
                IWebElement linkElement = driver.FindElement(By.XPath($"{mainXpath}[{i + 1}]//a[@class='a-size-base a-link-normal s-underline-text s-underline-link-text s-link-style a-text-normal']"));

                string currentTitle = driver.FindElement(By.XPath($"{mainXpath}[{i + 1}]//span[@class='a-size-medium a-color-base a-text-normal']")).Text;
                string priceWhole = driver.FindElement(By.XPath($"{mainXpath}[{i + 1}]//span[@class='a-price-whole']")).Text;
                string priceFraction = driver.FindElement(By.XPath($"{mainXpath}[{i + 1}]//span[@class='a-price-fraction']")).Text;
                string linkString = linkElement.GetAttribute("href");
                string priceFull = '$' + priceWhole + '.' + priceFraction;

                results.Add(new Item(currentTitle, priceFull, linkString));
            }

            return results;
        }

        public void PrintItems(List<Item> items)
        {
            for(int i = 0; i < items.Count; i++) 
            {
                Console.WriteLine($"Result: {i + 1}\n" + items[i].ToString());
            }
        }
    }
}