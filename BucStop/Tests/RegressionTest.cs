using BucStop.Services;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BucStop.Tests
{

    [TestFixture]
    public class RegressionTest
    {
        public AccessCode accessCode;
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        private void NavigateToLoginPage()
        {
            driver.Url = "https://localhost:7182/";
            var loginPage = driver.FindElement(By.Id("login"));
            loginPage.Click();
        }

        private void PerformLogin(string email)
        {
            var emailForm = driver.FindElement(By.Id("email"));
            //var loginButton = driver.FindElement(By.Id("loginButton"));
            emailForm.SendKeys(email);
            emailForm.Submit();
            //loginButton.Click();
        }
        /*
        private void ValidateAccessCode(string code)
        {
            var codeForm = driver.FindElement(By.Name("accessCode"));
            codeForm.SendKeys(code);
            codeForm.Submit();
        }
        */

        [Test]
        public void LoginWithValidCredentials()
        {
            //string code = accessCode.ToString();
            //accessCode = new AccessCode("member@etsu.edu");
            NavigateToLoginPage();
            PerformLogin("testEmail@etsu.edu");
            //ValidateAccessCode("123456");

            Assert.That(new Uri(driver.Url).Equals("https://localhost:7182/"));
        }

        // Add more test methods for different scenarios
    }
}

