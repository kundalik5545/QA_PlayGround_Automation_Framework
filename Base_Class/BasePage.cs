using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace QA_PlayGround.Base_Class
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait shortWait;
        protected WebDriverWait longWait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            shortWait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            longWait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
        }
    }

}