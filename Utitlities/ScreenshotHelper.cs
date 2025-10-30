csharp Utilities/ScreenshotHelper.cs
using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;

namespace QA_PlayGround.Utilities
{
    public static class ScreenshotHelper
    {
        public static string SaveScreenshot(IWebDriver driver, string prefix = "screenshot")
        {
            try
            {
                if (driver == null) return string.Empty;

                var takes = driver as ITakesScreenshot;
                if (takes == null) return string.Empty;

                string dir = TestContext.CurrentContext.WorkDirectory ?? Directory.GetCurrentDirectory();
                string ts = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
                string fileName = $"{prefix}_{ts}.png";
                string path = Path.Combine(dir, "Artifacts");
                Directory.CreateDirectory(path);
                string fullPath = Path.Combine(path, fileName);

                Screenshot ss = takes.GetScreenshot();
                ss.SaveAsFile(fullPath, ScreenshotImageFormat.Png);

                TestContext.AddTestAttachment(fullPath, "Screenshot on failure");
                return fullPath;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}