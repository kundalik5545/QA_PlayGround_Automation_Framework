using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using WebDriverManager.DriverConfigs.Impl;

namespace QA_PlayGround.Base_Class
{
    public class BaseTest
    {
        public IWebDriver driver;
        private readonly IConfiguration _configuration;
        private string _browserName;
        private string _baseUrl;
        private HttpClient httpClient;

        public BaseTest()
        {
            LogDetails("🚀-------------------------- New Test Running -----------------------------");
            LogDetails("Loaded configuration from appsettings.json");

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _browserName = _configuration["SeleniumSettings:BrowserName"] ?? "chrome";
            _baseUrl = _configuration["SeleniumSettings:BaseUrl"] ?? "https://www.qaplayground.com/";
        }

        [SetUp]
        public void OpenBrowser()
        {
            LogDetails($"Opening Browser - {_browserName}");

            InitBrowser(_browserName);

            if (driver == null)
            {
                throw new Exception("Driver initialization failed.");
            }

            httpClient = new HttpClient();

            driver.Manage().Window.Maximize();
            LogDetails("Browser Opened and Maximized");

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(_baseUrl);

            LogDetails($"Website Url entered");
        }


        [TearDown]
        public void CloseBrowser()
        {
            LogDetails("Closing the browser");
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose(); 
                httpClient.Dispose();
            }

            LogDetails("🚀-------------------------- Test Execution Completed -----------------------------");
        }

        private void InitBrowser(string browserName)
        {
            browserName = browserName.ToLower();

            switch (browserName)
            {
                case "chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;

                case "edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                    break;

                default:
                    throw new ArgumentException($"Unsupported browser: {browserName}");
            }
        }

        protected IWebDriver GetDriver()
        {
            return driver;
        }

        public static string GetFilePath(string folderName, string fileName)
        {
            string basePath = Directory.GetCurrentDirectory();
            LogDetails($"Base path of file is - {basePath}");

            string filePath = Path.Combine(basePath, folderName, fileName);

            LogDetails("Checking file is exist or not");
            if (File.Exists(filePath))
            {
                LogDetails($"File found: {filePath}");
                return filePath;
            }
            else
            {
                throw new FileNotFoundException($"File '{fileName}' not found in '{folderName}'");
            }

        }

        public static string GetFilePath2(string folderName, string fileName)
        {
            string path_BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string path_ProjectDirectory = Directory.GetParent(path_BaseDirectory)!.Parent!.Parent!.Parent!.FullName;

            string path_GivenFolder = Path.Combine(path_ProjectDirectory, folderName);

            string path_FileName = Path.Combine(path_GivenFolder, fileName);

            return path_FileName;
        }

        public static JObject JsonFileReader(string fileLocation)
        {
            var JsonData = File.ReadAllText(fileLocation);
            var testData = JObject.Parse(JsonData);

            return testData;
        }


        public static void LogDetails(string Message)
        {
            TestContext.Progress.WriteLine($"{Message}");
        }


        public async Task CheckLink(string url)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                LogDetails($"Checked url response - {response.StatusCode}");
                Assert.IsTrue(response.IsSuccessStatusCode, $"Broken link: {url} - Status Code: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Error checking link: {url} - Exception: {ex.Message}");
            }
        }
    }
}
