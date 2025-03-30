# Selenium C# Automation Framework with NUnit

## 📌 Overview
This is a **Selenium C# automation framework** that follows the **Page Object Model (POM)** and a **Data-Driven** approach using **JSON files** to fetch test data. The framework is built with **NUnit** as the test runner and is designed for efficient and scalable UI testing.

## 🔥 Key Features
- **Selenium with C# and NUnit** for UI test automation.
- **Page Object Model (POM)** to separate UI interactions.
- **Test Class, Base Class, and POM Class** structure for maintainability.
- **Data-Driven Testing** using JSON files.
- **Reusable Methods** for common actions.
- **QA Playground Demo Site** as the test application.

## 📂 Project Structure
```
AutomationFramework/
│-- Tests/                # Test classes with NUnit test cases
│   ├── LoginTests.cs     # Sample test case for login functionality
│   ├── RegistrationTests.cs
│
│-- Pages/                # Page Object Model (POM) classes
│   ├── LoginPage.cs      # Methods for login page interactions
│   ├── DashboardPage.cs  # Methods for dashboard interactions
│
│-- Base/                 # Base class to initialize WebDriver
│   ├── BaseTest.cs       # WebDriver setup and teardown
│
│-- TestData/             # JSON files for data-driven testing
│   ├── testdata.json     # Contains test input data
│
│-- Utilities/            # Helper classes for common methods
│   ├── JsonReader.cs     # Reads data from JSON files
│
│-- Drivers/              # WebDriver executables (Chrome, Edge, etc.)
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
  Install-Package Selenium.WebDriver
  Install-Package NUnit
  Install-Package NUnit3TestAdapter
  Install-Package Newtonsoft.Json
  ```
- Download the appropriate **WebDriver** (ChromeDriver, EdgeDriver, etc.) and place it in the `Drivers` folder.

### **2. Clone the Repository**
```sh
git clone https://github.com/yourusername/automation-framework.git
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
public class LoginPage
{
    private IWebDriver driver;
    public LoginPage(IWebDriver driver) { this.driver = driver; }

    private IWebElement UsernameField => driver.FindElement(By.Id("username"));
    private IWebElement PasswordField => driver.FindElement(By.Id("password"));
    private IWebElement LoginButton => driver.FindElement(By.Id("login"));

    public void Login(string username, string password)
    {
        UsernameField.SendKeys(username);
        PasswordField.SendKeys(password);
        LoginButton.Click();
    }
}
```

### **2. Test Class (NUnit)**
Test cases are defined in the `Tests/` folder. Example:
```csharp
[TestFixture]
public class LoginTests : BaseTest
{
    [Test]
    public void VerifyLogin()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Login("testuser", "password123");
        Assert.AreEqual("Dashboard", driver.Title);
    }
}
```

### **3. Base Class**
Handles WebDriver initialization and cleanup.
```csharp
public class BaseTest
{
    protected IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://qaplayground.com");
    }

    [TearDown]
    public void Teardown()
    {
        driver.Quit();
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
public class JsonReader
{
    public static JObject ReadJson()
    {
        string jsonText = File.ReadAllText("TestData/testdata.json");
        return JObject.Parse(jsonText);
    }
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

