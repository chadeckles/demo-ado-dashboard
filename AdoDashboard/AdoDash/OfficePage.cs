using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;


namespace AdoDash
{
    internal class OfficePage
    {
        private IWebDriver _driver;

        private WebDriverWait Wait => new WebDriverWait(_driver, TimeSpan.FromSeconds(30));

        public OfficePage(IWebDriver driver)
        {
            _driver = driver;
        }

        internal void GoTo()
        {
            _driver.Navigate().GoToUrl("https://www.office.com/?auth=1");
        }

        internal void Login()
        {
            var user = Environment.GetEnvironmentVariable("365_USER",
                EnvironmentVariableTarget.User);
            var pass = Environment.GetEnvironmentVariable("365_PASS",
                EnvironmentVariableTarget.User);

            var userName = Wait.Until(
                ExpectedConditions.ElementIsVisible(By.Id("i0116")));
            userName.SendKeys(user);

            var button = Wait.Until(
                ExpectedConditions.ElementIsVisible(By.Id("idSIButton9")));
            button.Click();

            var password = Wait.Until(
                ExpectedConditions.ElementIsVisible(By.Id("i0118")));
            password.SendKeys(pass);

            button = Wait.Until(
                ExpectedConditions.ElementIsVisible(By.Id("idSIButton9")));
            button.Click();

        }

        internal bool IsLoaded()
        {
            //O365_AppName
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("O365_AppName")));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}