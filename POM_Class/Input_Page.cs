using OpenQA.Selenium;
using QA_PlayGround.Base_Class;
using static SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace QA_PlayGround.POM_Class
{
    public class Input_Page : BasePage
    {
        public Input_Page(IWebDriver driver) : base(driver) { }

        //locators
        private readonly By InputPage_heading = By.XPath("//h2[text()='Input']");
        private readonly By Input_AnyMovieName = By.XPath("//input[@placeholder='Enter hollywood movie name']");
        private readonly By Input_AppendText = By.Id("appendText");
        private readonly By Input_InsideText = By.Id("insideText");


        //Methods
        public string InputPage_Heading()
        {
            string el = shortWait.Until(ElementIsVisible(InputPage_heading)).Text;
            return el;
        }

        public Input_Page Enter_MovieName(string text)
        {
            IWebElement el = shortWait.Until(ElementToBeClickable(Input_AnyMovieName));
            el.Clear();
            el.SendKeys(text);
            return this;
        }

        public Input_Page Enter_AppendNewText_PressTab(string text)
        {
            shortWait.Until(ElementToBeClickable(Input_AppendText)).SendKeys(text);
            shortWait.Until(ElementToBeClickable(Input_AppendText)).SendKeys(Keys.Tab);
            return this;
        }

        public string Verify_TextInside_Input()
        {
            IWebElement el = shortWait.Until(ElementToBeClickable(Input_InsideText));
            string e = el.GetAttribute("value");
            return e;
        }



    }
}
