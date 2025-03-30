using AngleSharp.Dom;
using OpenQA.Selenium;
using QA_PlayGround.Base_Class;
using QA_PlayGround.POM_Class;

namespace QA_PlayGround.TestClass
{
    public class PracticePage_Test : BaseTest
    {
        Home_Page? homePage;

        [Test]
        public async Task TC_01_Verify_BrokenLinks_OfPracticePage()
        {
            homePage = new Home_Page(GetDriver());

            IList<IWebElement> links = homePage.NavigateTo_PracticePage().PracticePage_Links();

           //string Urls = driver.Url;

           // LogDetails($"Url is {Urls}");

            List<Task> tasks = new List<Task>();

            foreach (var link in links)
            {
                string url = link.GetAttribute("href");

                if (!string.IsNullOrEmpty(url))
                {
                    LogDetails($"Checking page url - {url}");
                    tasks.Add(CheckLink(url));
                }
            }

            await Task.WhenAll(tasks);
        }
    }
}
