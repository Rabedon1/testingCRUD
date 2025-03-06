using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;
using System;
using Docker.DotNet.Models;

namespace TestInsert
{
    public class UnitTest1 : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public UnitTest1()
        {
            _driver = new ChromeDriver(); 
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)); 
        }

        [Fact]
        public void Create_Cliente()
        {
            _driver.Navigate().GoToUrl("https://localhost:7234/Cliente/Create");

           
            _wait.Until(driver => driver.FindElement(By.Id("Cedula"))).SendKeys("1751865823");
            _wait.Until(driver => driver.FindElement(By.Id("Apellidos"))).SendKeys("Perez Gomez");
            _wait.Until(driver => driver.FindElement(By.Id("Nombres"))).SendKeys("Joel Andrés");
            _wait.Until(driver => driver.FindElement(By.Id("FechaNacimiento"))).SendKeys("01/01/1990");
            _wait.Until(driver => driver.FindElement(By.Id("Mail"))).SendKeys("joel@example.com");
            _wait.Until(driver => driver.FindElement(By.Id("Telefono"))).SendKeys("0987654321");
            _wait.Until(driver => driver.FindElement(By.Id("Direccion"))).SendKeys("Av. Siempre Viva 742");

           
            var selectElement = new SelectElement(_wait.Until(driver => driver.FindElement(By.Name("Estado"))));
            selectElement.SelectByValue("True");


            _wait.Until(driver => driver.FindElement(By.CssSelector("button.btn.btn-primary"))).Click();





            _wait.Until(driver => !driver.Url.Contains("/Cliente/Create"));

            
            Assert.True(_driver.Url.Contains("https://localhost:7234/"), "La URL esperada no se cargó correctamente.");
        }

        [Fact]
        public void Edit_Cliente()
        {
            
            _driver.Navigate().GoToUrl("https://localhost:7234/Cliente/Edit?codigo=2"); 

       
            var cedula = _wait.Until(driver => driver.FindElement(By.Id("Cedula")));
            cedula.Clear();
            cedula.SendKeys("1751865824"); 

            var apellidos = _wait.Until(driver => driver.FindElement(By.Id("Apellidos")));
            apellidos.Clear();
            apellidos.SendKeys("Gomez Pérez");

            var nombres = _wait.Until(driver => driver.FindElement(By.Id("Nombres")));
            nombres.Clear();
            nombres.SendKeys("Andrés Alex"); 

            var fechaNacimiento = _wait.Until(driver => driver.FindElement(By.Id("FechaNacimiento")));
            fechaNacimiento.Clear();
            fechaNacimiento.SendKeys("02/02/1991"); 

            var mail = _wait.Until(driver => driver.FindElement(By.Id("Mail")));
            mail.Clear();
            mail.SendKeys("andres@example.com"); 

            var telefono = _wait.Until(driver => driver.FindElement(By.Id("Telefono")));
            telefono.Clear();
            telefono.SendKeys("0998765432"); 

            var direccion = _wait.Until(driver => driver.FindElement(By.Id("Direccion")));
            direccion.Clear();
            direccion.SendKeys("Calle Nueva 123"); 

           
            var selectElement = new SelectElement(_wait.Until(driver => driver.FindElement(By.Name("Estado"))));
            selectElement.SelectByValue("False");


            _wait.Until(driver => driver.FindElement(By.XPath("//button[text()='Guardar']"))).Click();



            _wait.Until(driver => !driver.Url.Contains("/Cliente/Edit"));

            
            Assert.True(_driver.Url.Contains("https://localhost:7234/Cliente"), "La URL esperada no se cargó correctamente.");
        }

        [Fact]
        public void Delete_Cliente()
        {
            
            _driver.Navigate().GoToUrl("https://localhost:7234/Cliente/Delete?codigo=11");

       
            var deleteButton = _wait.Until(driver => driver.FindElement(By.CssSelector("button.btn.btn-danger"))); 
            deleteButton.Click();  

            
            _wait.Until(driver => !driver.Url.Contains("/Cliente/Delete"));

           
            Assert.True(_driver.Url.Contains("https://localhost:7234/Cliente"), "La URL esperada no se cargó correctamente.");
            
     
        }




        public void Dispose()
        {
            _driver.Quit(); 
            _driver.Dispose();
        }
    }
}
