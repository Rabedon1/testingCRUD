using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Reqnroll;
using TDDTesting.Data;
using TDDTesting.Models;

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

    [Given("El cliente existe en la base de datos")]
    public void GivenElClienteExisteEnLaBaseDeDatos(DataTable dataTable)
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

            // Primero agregamos al cliente a la base de datos
            _clienteDAL.AddCliente(_cliente);

            _test.Log(Status.Pass, "Cliente agregado correctamente a la base de datos.");
        }
        catch (Exception ex)
        {
            _test.Log(Status.Fail, $"Error al agregar el cliente a la base de datos: {ex.Message}");
            throw;
        }
    }

    [When("Actualizo los datos del cliente")]
    public void WhenActualizoLosDatosDelCliente(DataTable dataTable)
    {
        try
        {
            var clienteData = dataTable.Rows[0];

            // Actualizamos los datos del cliente en la base de datos
            var clienteDb = _clienteDAL.GetClientes().FirstOrDefault(c => c.Cedula == _cliente.Cedula);
            if (clienteDb != null)
            {
                clienteDb.Apellidos = clienteData["Apellidos"];
                clienteDb.Nombres = clienteData["Nombres"];
                clienteDb.FechaNacimiento = DateTime.Parse(clienteData["FechaNacimiento"]);
                clienteDb.Mail = clienteData["Mail"];
                clienteDb.Telefono = clienteData["Telefono"];
                clienteDb.Direccion = clienteData["Direccion"];
                clienteDb.Estado = clienteData["Estado"] == "Activo";

                _clienteDAL.UpdateCliente(clienteDb);
            }

            _test.Log(Status.Pass, "Datos del cliente actualizados correctamente en la base de datos.");
        }
        catch (Exception ex)
        {
            _test.Log(Status.Fail, $"Error al actualizar los datos del cliente en la base de datos: {ex.Message}");
            throw;
        }
    }

    [Then("El resultado de la actualización en la BD")]
    public void ThenElResultadoDeLaActualizacionEnLaBd(DataTable dataTable)
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

            _test.Log(Status.Pass, "La actualización en la base de datos fue verificada correctamente.");
        }
        catch (Exception ex)
        {
            _test.Log(Status.Fail, $"Error al verificar la actualización en la base de datos: {ex.Message}");
            throw;
        }
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _extent.Flush();
    }
}
