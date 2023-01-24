using OpenQA.Selenium;
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
        public SearchBar SearchBar { get; set; }
        public Header Header { get; set; }
        public Filter Filter { get; set; }
        public List<ResultsItem> ResultsItems { get; set; }
        
        public ResultsPage(IWebDriver webDriver) 
        {
            driver = webDriver;
            SearchBar = new SearchBar(driver);
            Header = new Header();
            Filter = new Filter();
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
