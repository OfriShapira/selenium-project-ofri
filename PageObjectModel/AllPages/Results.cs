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
        public IList<double> GetAllPricesHigherThan(int num)
        {
            IList<double> resultPrices = new List<double>();
            IList<IWebElement> webPrices = driver.FindElements(By.ClassName("s-item__price"));
            for (int i = 1; i < webPrices.Count; i++)
            {
                string currentPrice = Regex.Match(webPrices[i].Text, @"\s+\d+(.)?\d+").Value;
                double price = double.Parse(currentPrice);
                if (price > num)
                {
                    resultPrices.Add(price);
                }
            }
            return resultPrices;
        }

        public void GetAllPricesHigherThanXPath(int num)
        {
            string xpath = "//span[@class='s-item__price' and" +
                $" number(translate(substring-after(., 'ILS '), ',', '')) > {num} or" +
                " number(translate(substring-before(substring-after(., 'ILS '), ' to'), ',', '') > " +
                $"{num})]/ancestor::div[@class='s-item__details clearfix']//preceding-sibling::a//span[@role='heading']";
            
            IList<IWebElement> titels = driver.FindElements(By.XPath(xpath));
            Console.WriteLine(titels.Count);

            foreach (IWebElement title in titels) 
            {
                Console.WriteLine(title.Text,"\n");
            }
        }

        public void FilterBy(int num)
        {
            IWebElement minFilter = driver.FindElement(By.Id("s0-52-12-0-1-2-6-0-8[3]-0-textrange-beginParamValue-textbox"));
            minFilter.SendKeys(Convert.ToString(num));
            IWebElement filterButton = driver.FindElement(By.XPath("//button[@title=\"Submit price range\"]"));
            filterButton.Click();
        }

        public void PrintAllItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void GetResultsBy(Dictionary<string, string> conditions)
        {
            List<Item> results = new List<Item>();

            string mainXpath = "(//span[@class='a-price' ";
            string closingXpath = "]//ancestor::div[@class='a-section a-spacing-small a-spacing-top-small'])";

            if (conditions.ContainsKey("Price_Lower_Then"))
            {
                mainXpath += $" and number(substring-before(substring((.), 2, 30), '$')) < {conditions["Price_Lower_Then"]}";
            }

            if (conditions.ContainsKey("PriceHigherThan"))
            {
                mainXpath += $" and number(substring-before(substring((.), 2, 30), '$')) >= {conditions["Price_Hiegher_OR_Equal_Then"]} ";
            }

            if (conditions.ContainsKey("Free_Shipping"))
            {
                string isFree = conditions["Free_Shipping"];
                if (isFree == "true")
                {
                    mainXpath += " and .//ancestor::div[@class='sg-row' and .//span[contains(text(), 'FREE Shipping')]] ";
                }
                else if (isFree == "false")
                {

                    mainXpath += " and .//ancestor::div[@class='sg-row' and .//span[not(contains(text(), 'FREE Shipping'))]] ";
                }
            }

            mainXpath += closingXpath;

            // Find the amount of items in the list
            IList<IWebElement> itemsList = driver.FindElements(By.XPath(mainXpath));

            for (int i = 0; i< itemsList.Count; i++) 
            {
                IWebElement aElement = driver.FindElement(By.XPath($"{mainXpath}[{i+1}]//a[@class='a-size-base a-link-normal s-underline-text s-underline-link-text s-link-style a-text-normal']"));
                
                string currentTitle = driver.FindElement(By.XPath($"{mainXpath}[{i+1}]//span[@class='a-size-medium a-color-base a-text-normal']")).Text;
                string priceWhole = driver.FindElement(By.XPath($"{mainXpath}[{i + 1}]//span[@class='a-price-whole']")).Text;
                string priceFraction = driver.FindElement(By.XPath($"{mainXpath}[{i + 1}]//span[@class='a-price-fraction']")).Text;
                string currentLink = aElement.GetAttribute("href");
                string priceFull = priceWhole + '.' + priceFraction;
                
                results.Add(new Item(currentTitle,priceFull , currentLink));
                Console.WriteLine($"result {i + 1}:" + results[i].ToString());
            }
        } 
    }
}