using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace TestingReqnroll.Utilities
{
    public static class WebDriverManager
    {
        public static IWebDriver GetDriver(string browser)
        {
            return browser.ToLower() switch
            {
                "chrome" => new ChromeDriver(),
                "firefox" => new FirefoxDriver(),
                _ => throw new ArgumentException($"Navegador no soportado: {browser}")
            };
        }
    }
}
