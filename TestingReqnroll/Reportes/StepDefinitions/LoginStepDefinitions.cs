/*using System;
using System.Linq.Expressions;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using Reqnroll;
using TestingReqnroll.Utilities;

namespace TestingReqnroll.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
            
        private IWebDriver _driver;
        private static ExtentReports _extent;
        private ExtentTest _text;
        private readonly ScenarioContext _scenarioContext;

        public LoginStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public void BeforeTestRun()
        {
            var spartReporter = new ExtentSparkReporter("ExtentReport.html");
            _extent = new ExtentReports();
            _extent = AttachReporter(spartReporter);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver = WebDriverManager.GetDriver("chrome");

            _test = _extent.CreateTest(_scenarioContext.ScenarioInfo.Title);
        }
        [Given("El usuario esta en la pagina del login")]
        public void GivenElUsuarioEstaEnLaPaginaDelLogin()
        {
            _driver.Navigate().GoToUrl("https://www.automationexercise.com/login");
            _test.Log(Status.Pass, "Usuario navega a la pagina del login");
        }

        [When("Ingresa las credenciales rabedon{int}@gmail.com y la contraseña {string}")]
        public void WhenIngresaLasCredencialesRabedonGmail_ComYLaContrasena(int p0, string p1)
        {
            _driver.FindElement(By.Name("email")).SendKeys(email);
            _driver.FindElement(By.Name("password")).SendKeys(password));
            _test.Log(Satus.Info, $"Usuario ingresa correo: {email} y contraseña: {password}");
        }

        [When("Hacer click en el boton de inicio de sesión")]
        public void WhenHacerClickEnElBotonDeInicioDeSesion()
        {
            try
            {
                bool isLoggedIn = _driver.FindElement(By.ClassName("user-info")) != null;
                _test.Log(Status.Pass, "Inicio de sesión exitoso");
            }
        }   CatchBlock ()

        [Then("Deberia ver un mensaje de error")]
        public void ThenDeberiaVerUnMensajeDeError()
        {
            
        }

        [AfterScenario]
        public void
    }
}*/
