using OpenQA.Selenium;

namespace SeleniumTask
{
    public class MainPage : PageObjectBase
    {
        private static readonly By contactUsButton = By.Id("contact-link");

        public MainPage(IWebDriver driver) : base(driver)
        {
        }

        public ContactUsPage ClickOnContactUs()
        {
            Driver.FindElement(contactUsButton).Click();
            return new ContactUsPage(Driver);
        }
    }
}
