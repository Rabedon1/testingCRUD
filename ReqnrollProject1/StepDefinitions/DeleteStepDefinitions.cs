using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using TDDTesting.Data;
using TDDTesting.Models;
using Microsoft.Build.Framework;
using FluentAssertions;

[Binding]
public class DeleteStepDefinitions
{
    private readonly ClienteDataAccessLayer _clienteDAL = new ClienteDataAccessLayer();
    private Cliente _cliente;
    private int _clienteID;
    private static ExtentReports _extent;
    private ExtentTest _test;
    private readonly ScenarioContext _scenarioContext;

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        var sparkReporter = new ExtentSparkReporter("DeleteTestReport.html");
        _extent = new ExtentReports();
        _extent.AttachReporter(sparkReporter);
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        _test = _extent.CreateTest(_scenarioContext.ScenarioInfo.Title);
    }

    public DeleteStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given("Se selecciona el cliente a eliminar")]
    public void GivenUnClienteRegistradoEnLaBaseDeDatos(Reqnroll.Table table)
    {
        var row = table.Rows[0]; // Tomar la primera fila de datos
        _cliente = new Cliente
        {
            Cedula = row["Cedula"],
            Apellidos = row["Apellidos"],
            Nombres = row["Nombres"],
            FechaNacimiento = DateTime.Parse(row["FechaNacimiento"]),
            Mail = row["Mail"],
            Telefono = row["Telefono"],
            Direccion = row["Direccion"],
            Estado = bool.Parse(row["Estado"])
        };

        // Insertar el cliente en la BD y guardar el ID generado
        _clienteID = _clienteDAL.AddCliente(_cliente);
    }

    [When("Se elimina el cliente de la db")]
    public void WhenEliminoElClienteDeLaBaseDeDatos()
    {
        _clienteDAL.DeleteCliente(_clienteID);
    }

    [Then("El cliente ya no debe existir en la db")]
    public void ThenElClienteYaNoDebeExistirEnLaBaseDeDatos()
    {
        var clienteBD = _clienteDAL.GetClienteById(_clienteID);
        clienteBD.Should().BeNull("porque el cliente ha sido eliminado de la base de datos");
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _extent.Flush();
    }
}