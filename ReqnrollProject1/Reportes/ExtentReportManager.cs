using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace ReqnrollTest.Reportes
{
    internal class ExtendReportManager
    {
        private static ExtentReports extent;
        private static ExtentTest test;

        private static string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reportes/TestResults", "reporte.html");

        public static void InitReport()
        {
            var sparkReporter = new ExtentSparkReporter(reportPath);

            extent = new ExtentReports();

            extent.AttachReporter(sparkReporter);

        }

        public static void StartTest(string testName)
        {
            test = extent.CreateTest(testName);
        }

        public static void LogStep(bool isSuccess, string stepDetails)
        {
            if (isSuccess)
                test.Log(Status.Pass, stepDetails);
            else
                test.Log(Status.Fail, stepDetails);

        }

        public static void FlushReport()
        {
            extent.Flush();
        }

    }
}
