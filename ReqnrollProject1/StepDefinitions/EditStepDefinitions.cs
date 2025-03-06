using System;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using Reqnroll;
using TDDTesting.Data;
using TDDTesting.Models;

namespace ReqnrollProject1.StepDefinitions
{
    [Binding]
    public class EditStepDefinitions
    {
        private readonly ClienteDataAccessLayer _clienteDAL = new ClienteDataAccessLayer();
        private Cliente _cliente;
        private static ExtentReports _extent;
        private ExtentTest _test;
        private readonly ScenarioContext _scenarioContext;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var sparkReporter = new ExtentSparkReporter("EditTestReport.html");
            _extent = new ExtentReports();
            _extent.AttachReporter(sparkReporter);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _test = _extent.CreateTest(_scenarioContext.ScenarioInfo.Title);
        }

        public EditStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("Editar los campos del formulario")]
        public void GivenEditarLosCamposDelFormulario(DataTable dataTable)
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

                _test.Log(Status.Pass, "Los campos del formulario para editar fueron llenados correctamente.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, $"Error al llenar los campos del formulario para editar: {ex.Message}");
                throw;
            }
        }

        [When("Actualizar usuario en la db")]
        public void WhenActualizarUsuarioEnLaDb()
        {
            try
            {
                _clienteDAL.UpdateCliente(_cliente);

                _test.Log(Status.Pass, "Cliente actualizado correctamente en la base de datos.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, $"Error al actualizar el cliente en la base de datos: {ex.Message}");
                throw;
            }
        }

        [Then("El resultado de la actualización en la db")]
        public void ThenElResultadoDeLaActualizacionEnLaDb(DataTable dataTable)
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

                _test.Log(Status.Pass, "La actualización del cliente en la base de datos fue verificada correctamente.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, $"Error al verificar la actualización del cliente en la base de datos: {ex.Message}");
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
