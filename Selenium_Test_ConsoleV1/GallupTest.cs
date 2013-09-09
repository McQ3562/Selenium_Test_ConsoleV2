using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Selenium_Test_ConsoleV1
{
    class GallupTest
    {
        public bool Login(IWebDriver driver)
        {
            string StartingURL = ConfigINI.GetValue("StartingURL");
            string StartingPageTitle = ConfigINI.GetValue("StartingPageTitle");

            driver.Navigate().GoToUrl(StartingURL);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return d.Title.ToLower().StartsWith(StartingPageTitle); });

            if (driver.Title == StartingPageTitle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
