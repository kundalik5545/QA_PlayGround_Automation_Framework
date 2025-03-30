using OpenQA.Selenium;
using QA_PlayGround.Base_Class;
using static SeleniumExtras.WaitHelpers.ExpectedConditions;
namespace QA_PlayGround.POM_Class
{
    public class Home_Page : BasePage
    {
        public Home_Page(IWebDriver driver) : base(driver) { }


        protected readonly By homePageTitle = By.XPath("//h1");
        protected readonly By allLinks = By.TagName("a");

        //practice page navigation
        protected readonly By exploreMore = By.LinkText("Explore More");

        public string HomePage_Title()
        {
            IWebElement el = shortWait.Until(ElementIsVisible(homePageTitle));
            string homePageTitle_Text = el.Text;
            return homePageTitle_Text;
        }

        public IList<IWebElement> HomePage_AllLinks()
        {
            return shortWait.Until(driver =>
           {
               var elements = driver.FindElements(allLinks);
               return elements.Count > 0 ? elements : null;
           });
        }

        public Practice_Page NavigateTo_PracticePage()
        {
            shortWait.Until(ElementIsVisible(exploreMore)).Click();
            return new Practice_Page(driver);
        }
    }
}
