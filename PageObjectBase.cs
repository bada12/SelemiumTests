using OpenQA.Selenium;

namespace SeleniumTask
{
    public abstract class PageObjectBase
    {
        protected readonly IWebDriver Driver;

        public PageObjectBase(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}
