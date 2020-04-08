using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTask
{
    public enum SubjectHeadingOption
    {
        Choose = 0,
        Webmaster = 1,
        Customer = 2
    }

    public class ContactUsPage : PageObjectBase
    {
        private static readonly By subjectHeading = By.Id("id_contact");
        private static readonly By email = By.Id("email");
        private static readonly By orderReference = By.Id("id_order");
        private static readonly By message = By.Id("message");
        private static readonly By send = By.Id("submitMessage");
        private static readonly By alert = By.XPath(@"//div[@class='alert alert-danger']");

        private WebDriverWait wait;

        public ContactUsPage(IWebDriver driver) : base(driver)
        {
        }

        public ContactUsPage SelectSubjectHeading(SubjectHeadingOption choose)
        {
            Driver.FindElement(subjectHeading).Click();
            Driver.FindElement(subjectHeading)
                .FindElement(By.XPath($"//option[@value='{Convert.ToInt32(choose)}']"))
                .Click();

            return new ContactUsPage(Driver);
        }

        public ContactUsPage InputEmailField(string emailAddress)
        {
            Driver.FindElement(email).Click();
            Driver.FindElement(email).SendKeys(emailAddress);
            Driver.FindElement(email).SendKeys(Keys.Tab);

            return new ContactUsPage(Driver);
        }

        public ContactUsPage InputOrderReference(string orderReferenceString)
        {
            Driver.FindElement(orderReference).Click();
            Driver.FindElement(orderReference).SendKeys(orderReferenceString);
            Driver.FindElement(orderReference).SendKeys(Keys.Tab);

            return new ContactUsPage(Driver);
        }

        public ContactUsPage InputMessage(string messageString)
        {
            Driver.FindElement(message).Click();
            Driver.FindElement(message).SendKeys(messageString);
            Driver.FindElement(message).SendKeys(Keys.Tab);

            return new ContactUsPage(Driver);
        }

        public ContactUsPage ClickOnSend()
        {
            Driver.FindElement(send).Click();

            return new ContactUsPage(Driver);
        }

        public bool IsMessageSend()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
            wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(250));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            bool isSentFailure = false;

            try
            {
                isSentFailure = wait.Until(x =>
                        x.FindElements(alert).Any());
            }
            catch(WebDriverException)
            {
                Console.WriteLine("Message has not sent. You have some error with data");
            }

            return !isSentFailure;
        }
    }
}


