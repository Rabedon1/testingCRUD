using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;
using Xunit.Sdk;

namespace TestClientes
{
    public class UnitTest1
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public UnitTest1()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddExcludedArgument("enable-automation");
            options.AddArgument("--start-maximized");

            driver = new ChromeDriver(@"C:\WebDriver", options);
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Fact]
        public void Test_navegador()
        {
            driver.Navigate().GoToUrl("https://www.google.com/");
            var buscarTexto = driver.FindElement(By.Name("q"));

            buscarTexto.SendKeys("Selenium");
            buscarTexto.SendKeys(Keys.Enter);

            var resultados = wait.Until(d =>
            {
                var elements = d.FindElements(By.CssSelector("h3"));
                return elements.Count > 0 ? elements : null;
            });

            Assert.True(resultados.Count > 0, "No se encontraron resultados");

            foreach (var resultado in resultados)
            {
                Console.WriteLine("Test");
            }
        }



        public bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        }

        [Theory]
        [InlineData("joel.darguello@gmail.com", true)]
        [InlineData("joel.darguello@test.com", true)]

        [InlineData("joel.darguellogmail.com", false)]
        [InlineData("", false)]
        public void ValidateEmailMustDetectValidEmail(string email, bool IsValid)
        {
            bool result = IsEmailValid(email);
            Assert.Equal(IsValid, result);
        }
    }
}