using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace TestCliente
{
    public class LoginTest : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public LoginTest()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddExcludedArgument("enable-automation");
            options.AddArgument("--start-maximized");

            _driver = new ChromeDriver(@"C:\WebDriver", options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public bool EsMailValido(string email)
        {
            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        [Theory]
        [InlineData("usuario@gmail.com", true)]
        [InlineData("usuario@espe.edu.ec", true)]
        [InlineData("correo@incorrecto", false)]
        [InlineData("sinformatocorrecto", false)]
        public void Test_ValidarCorreos(string email, bool esperado)
        {
            bool resultado = EsMailValido(email);
            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void Test_LoginCamposVacios()
        {
            _driver.Navigate().GoToUrl("https://www.automationexercise.com/login");

            var botonLogin = _driver.FindElement(By.CssSelector("button[data-qa='login-button']"));
            botonLogin.Click();

            Thread.Sleep(3000);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
           // var mensajeError = wait.Until(d => d.FindElement(By.XPath("//p[contains(text(),'Your email or password is incorrect!')]")));

           // Assert.True(mensajeError.Displayed, "No se mostró el mensaje de error esperado.");
           
        }

        [Fact]
        
        public void Test_LoginCorreoInvalido()
        {
            string email = "alex.234455.com"; 


            
            Assert.False(EsMailValido(email), $"El correo {email} es inválido.");

            _driver.Navigate().GoToUrl("https://www.automationexercise.com/login");

            var emailInput = _driver.FindElement(By.CssSelector("input[data-qa='login-email']"));
            var passwordInput = _driver.FindElement(By.CssSelector("input[data-qa='login-password']"));
            var botonLogin = _driver.FindElement(By.CssSelector("button[data-qa='login-button']"));

            emailInput.SendKeys(email);
            passwordInput.SendKeys("pepepeppepe");
            botonLogin.Click();

            Thread.Sleep(3000);
            var mensajeError = _wait.Until(d => d.FindElement(By.XPath("//p[contains(text(), concat('Incluye \"@\" en la dirección de correo electrónico. En ', 'alex.234455.com', ' falta un simbolo \"@\".'))]")));


            Assert.True(mensajeError.Displayed, "No se mostró el mensaje de error esperado.");

        }



        [Fact]
        public void Test_LoginContrasenaIncorrecta()
        {
            _driver.Navigate().GoToUrl("https://www.automationexercise.com/login");

            var emailInput = _driver.FindElement(By.CssSelector("input[data-qa='login-email']"));
            var passwordInput = _driver.FindElement(By.CssSelector("input[data-qa='login-password']"));
            var botonLogin = _driver.FindElement(By.CssSelector("button[data-qa='login-button']"));

            emailInput.SendKeys("rabedon2@espe.edu.ec");
            passwordInput.SendKeys("Roberto45");
            botonLogin.Click();


            Thread.Sleep(3000);
            var mensajeError = _wait.Until(d => d.FindElement(By.XPath("//p[contains(text(),'Your email or password is incorrect!')]")));
            Assert.True(mensajeError.Displayed, "No se mostró el mensaje de error esperado.");
        }
[Fact]
        public void Test_LoginExitoso()
        {
            string email = "rabedon1@espe.edu.ec"; // Correo válido

            // Validación del correo
            Assert.True(EsMailValido(email), $"El correo {email} no pasó la validación.");

            _driver.Navigate().GoToUrl("https://www.automationexercise.com/login");

            var emailInput = _driver.FindElement(By.CssSelector("input[data-qa='login-email']"));
            var passwordInput = _driver.FindElement(By.CssSelector("input[data-qa='login-password']"));
            var botonLogin = _driver.FindElement(By.CssSelector("button[data-qa='login-button']"));

            emailInput.SendKeys(email);
            passwordInput.SendKeys("Roberto25.");
            botonLogin.Click();

            Thread.Sleep(3000);

            IWebElement logoutLink = _driver.FindElement(By.XPath("//a[@href='/logout'][contains(text(),'Logout')]"));

            Assert.True(logoutLink.Displayed, "El mensaje de logout no se mostró.");
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
