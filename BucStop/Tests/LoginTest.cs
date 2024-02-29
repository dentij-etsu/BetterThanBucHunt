// these should be the only references needed when making tests.
using OpenQA.Selenium;
using NUnit.Framework;

/* Explanation of what's going on here:
 * 
 * We're using Selenium WebDriver and NUnit to create our testing script. 
 * 
 * The Selenium library consists of all the things we need to actually use the WebDriver,
 * In our case we'll be using Chrome since it's the most popular. 
 * I also opted to do this all in C# since BucStop is a .NET project. 
 * 
 * NUnit is typically used for unit testing, however we can use it in this case to 
 * better organize our different automated unit tests. 
 */
namespace BucStop.Tests
{
    // allows the test class to be recognized by visual studio.
    [TestFixture]
    public class LoginTest
    {
        // one time setup and teardown, you'd customize these if something needs to occur before a test
        [OneTimeSetUp]
        public void Init()
        { /* ... */ }

        [OneTimeTearDown]
        public void Cleanup()
        { /* ... */ }

        // happy path test for login. 
        [Test]
        public void Login()
        {
            // create an instance of a chrome webdriver 
            var driver = new OpenQA.Selenium.Chrome.ChromeDriver();

            // set the url to the website you want to test (deployed ver. of bucstop)
            driver.Url = "http://18.233.180.198";

            /* gen. workflow: 
             * finds the login button, clicks it...
             * finds email textbox, clicks & autofills... 
             * lastly, submits the email. 
             */

            var loginButton = driver.FindElement(By.Id("login"));
            loginButton.Click();


            var email = driver.FindElement(By.Id("email"));
            email.Click();
            email.SendKeys("member@etsu.edu");
            email.Submit();

            /* Uri (Uniform Resource Identifier): 
             * a way to identify or locate a resource on the internet.
             * use this to compare the driver url to the current url after the test workflow.
             */
            var uri = new Uri(driver.Url);

            // checking that the url is equal to the corresponding url after the user logs in.
            Assert.That(uri.Equals("http://18.233.180.198"));

            // once the test is complete, close the driver.
            driver.Quit();
        }
    }
}