using OpenQA.Selenium;
using QA_PlayGround.Base_Class;
using static SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace QA_PlayGround.POM_Class
{
    public class Practice_Page : BasePage
    {
        public Practice_Page(IWebDriver driver) : base(driver) { }

        //Locators
        protected readonly By PracticePageLinks = By.TagName("a");

        //Practice page navigation links locators
        protected readonly By InputPage_Link = By.LinkText("Edit");

        public IList<IWebElement> PracticePage_Links()
        {
            return shortWait.Until(driver =>
              {
                  var elements = driver.FindElements(PracticePageLinks);
                  return elements.Count > 0 ? elements : null;
              });
        }

        public Input_Page NavigateTo_InputPage()
        {
            shortWait.Until(ElementIsVisible(InputPage_Link)).Click();
            return new Input_Page(driver);
        }

        
    }
}
