using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;


namespace AdoDash
{
    internal class OfficeHomePage
    {
        private IWebDriver _driver;
        private WebDriverWait Wait => new WebDriverWait(_driver, TimeSpan.FromSeconds(10));


        public OfficeHomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement OneNote => 
            Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a#ShellOneNoteOnline_link")));
    }
}