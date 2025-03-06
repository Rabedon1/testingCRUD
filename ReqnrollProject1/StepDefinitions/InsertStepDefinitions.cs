using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Reqnroll;
using TDDTesting.Data;
using TDDTesting.Models;

namespace ReqnrollProject1.StepDefinitions
{
    [Binding]
    public class InsertStepDefinitions
    {
        private readonly ClienteDataAccessLayer _clienteDAL = new ClienteDataAccessLayer();
        private Cliente _cliente;
        private static ExtentReports _extent;
        private ExtentTest _test;
        private readonly ScenarioContext _scenarioContext;
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var sparkReporter = new ExtentSparkReporter("InsertTestReport.html");
            _extent = new ExtentReports();
            _extent.AttachReporter(sparkReporter);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _test = _extent.CreateTest(_scenarioContext.ScenarioInfo.Title);
        }

        public InsertStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("LLenar los campos del formulario")]
        public void GivenLLenarLosCamposDelFormulario(DataTable dataTable)
        {
            try
            {
                var clienteData = dataTable.Rows[0];

                _cliente = new Cliente
                {
                    Cedula = clienteData["Cedula"],
                    Apellidos = clienteData["Apellidos"],
                    Nombres = clienteData["Nombres"],
                    FechaNacimiento = DateTime.Parse(clienteData["FechaNacimiento"]),
                    Mail = clienteData["Mail"],
                    Telefono = clienteData["Telefono"],
                    Direccion = clienteData["Direccion"],
                    Estado = clienteData["Estado"] == "Activo"
                };

                _test.Log(Status.Pass, "Los campos del formulario fueron llenados correctamente.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, $"Error al llenar los campos del formulario: {ex.Message}");
                throw;
            }
        }

        [When("Registro de usuario en la db")]
        public void WhenRegistroDeUsuarioEnLaDb()
        {
            try
            {

                _clienteDAL.AddCliente(_cliente);

                _test.Log(Status.Pass, "Registro de usuario realizado correctamente en la base de datos.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, $"Error al registrar el usuario en la base de datos: {ex.Message}");
                throw;
            }
        }

        [Then("El resultado del registro en la db")]
        public void ThenElResultadoDelRegistroEnLaDb(DataTable dataTable)
        {
            try
            {
                var clienteData = dataTable.Rows[0];
                var clienteDb = _clienteDAL.GetClientes().FirstOrDefault(c => c.Cedula == _cliente.Cedula);

                Assert.NotNull(clienteDb);

                Assert.Equal(clienteData["Cedula"], clienteDb.Cedula);
                Assert.Equal(clienteData["Apellidos"], clienteDb.Apellidos);
                Assert.Equal(clienteData["Nombres"], clienteDb.Nombres);
                Assert.Equal(
                    DateTime.ParseExact(clienteData["FechaNacimiento"], "yyyy-MM-dd", null).ToString("dd/MM/yyyy"),
                    clienteDb.FechaNacimiento.ToString("dd/MM/yyyy")
                );

                Assert.Equal(clienteData["Mail"], clienteDb.Mail);
                Assert.Equal(clienteData["Telefono"], clienteDb.Telefono);
                Assert.Equal(clienteData["Direccion"], clienteDb.Direccion);
                Assert.Equal(clienteData["Estado"], clienteDb.Estado ? "Activo" : "Inactivo");

                _test.Log(Status.Pass, "El registro en la base de datos fue verificado correctamente.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, $"Error al verificar el registro en la base de datos: {ex.Message}");
                throw;
            }


        }
        [AfterScenario]
        public void AfterScenario()
        {
            _extent.Flush();
        }
    }
}
