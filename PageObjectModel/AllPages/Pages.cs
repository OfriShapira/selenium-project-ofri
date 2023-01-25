using OpenQA.Selenium;
using PageObjectModel.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModel.AllPages
{
    class Pages
    {
        private IWebDriver driver;
        private Home home;
        private Results results;

        public Pages(IWebDriver driver)
        {
            this.driver = driver;
        }

        public Home Home
        {
            get
            {
                if (home == null)
                {
                    home = new Home(driver);
                }
                return home;
            }
        }

        public Results Results
        {
            get
            {
                if (results == null)
                {
                    results = new Results(driver);
                }
                return results;
            }
        }

    
    }
}
