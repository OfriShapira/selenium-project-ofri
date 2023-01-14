using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModel
{
     class ResultsItem
    {
        private IWebDriver driver;
        public ResultsItem(IWebDriver webDriver) 
        {
            driver = webDriver;  
        }
    }
}
