# Selenium C# Automation Framework with NUnit

## 📌 Overview
This is a **Selenium C# automation framework** that follows the **Page Object Model (POM)** and a **Data-Driven** approach using **JSON files** to fetch test data. The framework is built with **NUnit** as the test runner and is designed for efficient and scalable UI testing.

## 🔥 Key Features
- **Selenium with C# and NUnit** for UI test automation.
- **Page Object Model (POM)** to separate UI interactions.
- **Test Class, Base Class, and POM Class** structure for maintainability.
- **Data-Driven Testing** using JSON files.
- **Reusable Methods** for common actions.
- [**QA Playground Demo Site**](https://www.qaplayground.com/practice) as the test application.

## 📂 Project Structure
```
AutomationFramework/
│-- TestClass/            # Test classes with NUnit test cases
│   ├── Input_Tests.cs    # Sample test case for Input page functionality
│   ├── Button_Tests.cs
│
│-- POM_Class/            # Page Object Model (POM) classes
│   ├── InputPage.cs      # Methods for Input page interactions
│   ├── DashboardPage.cs  # Methods for Input Page interactions
│
│-- Base_Class_/          # Base class to initialize WebDriver
│   ├── BasePage.cs       # Short waits stored to synchronise process
│   ├── BaseTest.cs       # WebDriver setup and teardown
│
│-- TestData/             # JSON files for data-driven testing
│   ├── testdata.json     # Contains test input data
│
│-- Utilities/            # Helper classes for common methods
│   ├── JsonReader.cs     # Reads data from JSON files
│
│-- Reports/              # Test execution reports
│
│-- README.md             # Project documentation
```

## 🛠️ Setup & Installation
### **1. Prerequisites**
- Install **Visual Studio** with .NET support.
- Install **Selenium WebDriver** & **NUnit** via NuGet:
  ```sh
  Install-Package NUnit
  Install-Package NUnit3TestAdapter
  Install-Package Newtonsoft.Json
  Install-Package Selenium.WebDriver
  Install-Package Selenium.Support
  Install-Package WebDriverManager
  Install-Package DotNetSeleniumExtras.WaitHelpers
  Install-Package Bogus
  Install-Package Microsoft.Net.Http
  ```
- Download the appropriate **WebDriver** (ChromeDriver, EdgeDriver, etc.) and place it in the `Drivers` folder.

### **2. Clone the Repository**
```sh
git clone https://github.com/kundalik5545/QA_PlayGround_Automation_Framework.git
cd automation-framework
```

### **3. Run Tests**
- Open the project in **Visual Studio**.
- Build the solution.
- Run tests using NUnit Test Explorer **or** via command line:
  ```sh
  dotnet test
  ```

## 📌 Framework Components
### **1. Page Object Model (POM)**
Each webpage has a dedicated class in the `Pages/` folder, encapsulating element locators and methods. Example:
```csharp
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
       
    }
}

```

### **2. Test Class (NUnit)**
Test cases are defined in the `Tests/` folder. Example:
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QA_PlayGround.Base_Class;
using QA_PlayGround.POM_Class;

namespace QA_PlayGround.TestClass
{
    public class Input_Test : BaseTest
    {
        Home_Page? homePage;

        [Test]
        public void Verify_EnterMoviewName()
        {
            homePage = new Home_Page(GetDriver());

            homePage!
                .NavigateTo_PracticePage()
                .NavigateTo_InputPage()
                .Enter_MovieName("IronMan");

            Thread.Sleep(3000);
        }

        [Test] 
        public void AppendText_AndPress_TabKeys()
        {
            homePage = new Home_Page(GetDriver());

            homePage!
                .NavigateTo_PracticePage()
                .NavigateTo_InputPage()
                .Enter_AppendNewText_PressTab("Hello");

            Thread.Sleep(3000);
        }
    }
}

```

### **3. Base Class**
#### **1. Base Test**
Handles WebDriver initialization and cleanup.
```csharp
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

```
#### **2. Base Page**
```csharp
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
```
### **4. Data-Driven Testing using JSON**
Test data is stored in a JSON file (`testdata.json`). Example:
```json
{
  "username": "testuser",
  "password": "password123"
}
```
To read JSON data in C#:
```csharp
public static JObject JsonFileReader(string fileLocation)
    {
        var JsonData = File.ReadAllText(fileLocation);
        var testData = JObject.Parse(JsonData);

        return testData;
    }

```

## 📊 Test Reporting
- Use NUnit's built-in reporting or integrate with **Extent Reports** for better insights.

## 🚀 Future Enhancements
- Implement **Parallel Execution** using NUnit.
- Integrate with **CI/CD pipelines** (GitHub Actions, Azure DevOps).
- Extend **Data-Driven Testing** with CSV and Excel support.

## 📜 License
This project is open-source. Feel free to modify and enhance it as per your requirements.

## 📩 Contact
For any queries, reach out at **randomcoders1@gmail.com**.

