using System;
using System.Threading;
using System.Linq;

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTask
{
    [TestFixture]
    public class BaseTest
    {
        private readonly IWebDriver driver;
        private static readonly string url = @"http://automationpractice.com/index.php";
        private WebDriverWait wait;

        public BaseTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(3000);
            driver.Navigate().GoToUrl(url);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Thread.Sleep(3000);
            driver.Quit();
        }


        [TestCase(true, SubjectHeadingOption.Customer, "Dkfk@gemg.comg","gnejg","kgekmgkengj")]
        [TestCase(false, SubjectHeadingOption.Customer, "", "gnejg", "kgekmgkengj")]
        public void Test(bool isPositive, SubjectHeadingOption subjectHeadingOption, 
                           string email, string orderReference, string message)
        {
            MainPage mainPage = new MainPage(driver);
            ContactUsPage contactUsPage = mainPage.ClickOnContactUs();
            bool isMesssageSent = contactUsPage
                .SelectSubjectHeading(subjectHeadingOption)
                .InputEmailField(email)
                .InputOrderReference(orderReference)
                .InputMessage(message)
                .ClickOnSend()
                .IsMessageSend();
            Assert.That(isMesssageSent, Is.EqualTo(isPositive),
                    $"Message has sent: {(isMesssageSent ? "successfully" : "unsuccessfully")}");
        }
    }
}
