using OpenQA.Selenium;
using QA_PlayGround.Base_Class;
using QA_PlayGround.POM_Class;

namespace QA_PlayGround.TestClass
{
    public class HomePage_Test : BaseTest
    {
        Home_Page? homePage; 

        [TestCaseSource(nameof(GetData01), new object[] { "TC_01_Verify_HomePageTitle" })]
        [Test]
        public void TC_01_Verify_HomePageTitle(string expectedPageTitle)
        {
            LogDetails("Home page object created and initialized");
            homePage = new Home_Page(GetDriver());

            string actualPageTitle = homePage!
                                          .HomePage_Title();

            LogDetails($"Actual Page title captured and stored - {actualPageTitle}");

            LogDetails($"Comparing actual and expected page tiles");

            Assert.That(actualPageTitle, Is.EqualTo(expectedPageTitle));

        }

        protected static IEnumerable<TestCaseData> GetData01(string testCaseName)
        {
            string folderName = "TestData";
            string fileName = "BasicDetails.json";
            string fileLocation = GetFilePath(folderName, fileName);

            var testData = JsonFileReader(fileLocation);
            var testCases = testData[testCaseName];

            foreach (var testCase in testCases!)
            {
                string expectedPageTitle = testCase["pageTitle"]!.ToString();

                yield return new TestCaseData(expectedPageTitle);
            }

        }

        [Test]
        public async Task TC_02_Verify_BrokenLinks()
        {
            homePage = new Home_Page(GetDriver());
            IList<IWebElement> links = homePage!.HomePage_AllLinks(); 

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
