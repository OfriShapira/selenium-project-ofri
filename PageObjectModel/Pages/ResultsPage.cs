﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using PageObjectModel.Pages;
using PageObjectModel.Components;

namespace PageObjectModel.Pages
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

        public void FilterBy(int num)
        {
            IWebElement minFilter = driver.FindElement(By.Id("s0-52-12-0-1-2-6-0-8[3]-0-textrange-beginParamValue-textbox"));
            minFilter.SendKeys(Convert.ToString(num));
            IWebElement filterButton = driver.FindElement(By.XPath("//button[@title=\"Submit price range\"]"));
            filterButton.Click();
        }

        public void PrintAllItems(List<ResultsItem> items)
        {
            foreach (ResultsItem item in items) 
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void GetItemList()
        {
            IList<ResultsItem> items = new List<ResultsItem>();
            IList<IWebElement> webPrices = driver.FindElements(By.ClassName("s-item__price"));
            IList<IWebElement> itemTitles = driver.FindElements(By.ClassName("s-item__title"));
            Console.WriteLine(webPrices.Count + ", " + itemTitles.Count);
            for(int i = 1; i < webPrices.Count; i++) 
            {
                string currentPrice = Regex.Match(webPrices[i].Text, @"\s+\d+(.)?\d+").Value;
                double price = double.Parse(currentPrice);
                items.Add(new ResultsItem(itemTitles[i].Text, price));
                Console.WriteLine(items[i-1].ToString());
            }
        }
    }
}
