using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Selenium_Test_ConsoleV1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new instance of the Firefox driver.
            IWebDriver driver = new FirefoxDriver();

            //Configure implicit wait
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            ConfigINI.Load(ConfigINI.GetPath()+"\\GallupTest.ini");

            GallupTest test = new GallupTest();
            test.Login(driver);

            driver.Quit();
        }

        public void test()
        {
            // Create a new instance of the Firefox driver.
            IWebDriver driver = new FirefoxDriver();

            //Configure implicit wait
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            //Navigate to URL
            driver.Navigate().GoToUrl("http://www.google.com/");

            // Find the text input element by its name
            IWebElement query = driver.FindElement(By.Name("q"));

            // Enter something to search for
            query.SendKeys("Cheese");

            // Now submit the form. WebDriver will find the form for us from the element
            //query.Submit();
            //Submit did not work found search button name and manually clicked it
            IWebElement search = driver.FindElement(By.Name("btnG"));
            search.Click();

            // Google's search is rendered dynamically with JavaScript.
            // Wait for the page to load, timeout after 10 seconds
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return d.Title.ToLower().StartsWith("cheese"); });
            
            // Should see: "Cheese - Google Search"
            System.Console.WriteLine("Page title is: " + driver.Title);

            //Close the browser
            driver.Quit();

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form_Main());
        }
    }
}
