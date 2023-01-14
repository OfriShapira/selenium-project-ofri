using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Edge;

namespace PageObjectModel
{
    class BrowserFactory
    {
        private static IDictionary<string, IWebDriver> drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver driver;

        public IWebDriver Driver
        {
            get
            {
                if (driver == null)
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
                return driver;
            }
            private set
            {
                driver = value;
            }
        }

        public IDictionary<string, IWebDriver> Drivers
        {
            get
            {
                return drivers;
            }
            private set
            {
                drivers = value;
            }
        }

        public void InitBrowser(string browserName)
        {
            switch (browserName.ToLower())
            {
                case "firefox":
                    if (!Drivers.ContainsKey("firefox"))
                    {
                        driver = new FirefoxDriver(@".\Drivers");
                        drivers.Add("firefox", driver);
                    }
                    break;
                case "ie":
                    if (!Drivers.ContainsKey("ie"))
                    {
                        driver = new EdgeDriver(@".\Drivers");
                        drivers.Add("ie", driver);
                    }
                    break;
                case "chrome":
                default:
                    if (!Drivers.ContainsKey("chrome"))
                    {
                        driver = new ChromeDriver(@".\Drivers");
                        drivers.Add("chrome", driver);
                    }
                    break;
            }
        }

        public void LoadApplication(string url, string driverName)
        {

            if (Drivers.Count() > 0)
            {
                if (drivers.ContainsKey(driverName))
                {
                    drivers[driverName].Url = url;
                }
            }
        }

        public static void CloseAllDrivers()
        {
            foreach (var key in drivers.Keys)
            {
                drivers[key].Close();
                drivers[key].Quit();
            }
        }
    }
}

