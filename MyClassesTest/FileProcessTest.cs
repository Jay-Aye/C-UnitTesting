using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System.Configuration;
using System.IO;

namespace MyClassesTest
{
  [TestClass]
  public class FileProcessTest
  {
    private const string BAD_FILE_NAME = @"C:\BadFileName.bad";
    private string _GoodFileName;

    #region Class Initialize and Cleanup
    [ClassInitialize]
    public static void ClassInitialize(TestContext tc)
    {
      tc.WriteLine("In the Class Initialize.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }
    #endregion

    #region Test Initialize and Cleanup
    [TestInitialize]
    public void TestInitialize()
    {
      if (TestContext.TestName.StartsWith("FileNameDoesExist")) {
        SetGoodFileName();
        if (!string.IsNullOrEmpty(_GoodFileName)) {
          TestContext.WriteLine("Creating File: " + _GoodFileName);
          File.AppendAllText(_GoodFileName, "Some Text");
        }
      }
    }

    [TestCleanup]
    public void TestCleanup()
    {
      if (TestContext.TestName.StartsWith("FileNameDoesExist")) {

        if (!string.IsNullOrEmpty(_GoodFileName)) {
          TestContext.WriteLine("Deleting File: " + _GoodFileName);
          File.Delete(_GoodFileName);
        }
      }
    }
    #endregion

    public TestContext TestContext { get; set; }

    [TestMethod]
    [DataSource("System.Data.SqlClient",
      "Server=Localhost;Database=Sandbox;Integrated Security=Yes",
      "tests.FileProcessTest",
      DataAccessMethod.Sequential)]
    public void FileExistsTestFromDB()
    {
      FileProcess fp = new FileProcess();
      string fileName;
      bool expectedValue;
      bool causesException;
      bool fromCall;

      // Get values from data row
      fileName = TestContext.DataRow["FileName"].ToString();
      expectedValue = Convert.ToBoolean(TestContext.DataRow["ExpectedValue"]);
      causesException = Convert.ToBoolean(TestContext.DataRow["CausesException"]);

      // Check assertion
      try {
        fromCall = fp.FileExists(fileName);
        Assert.AreEqual(expectedValue, fromCall,
          "File Name: " + fileName +
          " has failed it's existence test in test: FileExistsTestFromDB()");
      }
      catch (AssertFailedException ex) {
        // Rethrow assertion
        throw ex;
      }
      catch (ArgumentNullException) {
        // See if method was expected to throw an exception
        Assert.IsTrue(causesException);
      }
    }

    [TestMethod]
    [Description("Check to see if a file does exist.")]
    [Owner("Pauls")]
    [Priority(0)]
    [TestCategory("NoException")]
    public void FileNameDoesExist()
    {
      FileProcess fp = new FileProcess();
      bool fromCall;

      //TestContext.WriteLine("Creating the file: " + _GoodFileName);
      //File.AppendAllText(_GoodFileName, "Some Text");
      TestContext.WriteLine("Testing the file: " + _GoodFileName);
      fromCall = fp.FileExists(_GoodFileName);
      //TestContext.WriteLine("Deleting the file: " + _GoodFileName);
      //File.Delete(_GoodFileName);

      Assert.IsTrue(fromCall);
    }

    private const string FILE_NAME = @"FileToDeploy.txt";

    [TestMethod]
    [Owner("PaulS")]
    [DeploymentItem(FILE_NAME)]
    public void FileNameDoesExistUsingDeploymentItem()
    {
      FileProcess fp = new FileProcess();
      string fileName;
      bool fromCall;

      fileName = TestContext.DeploymentDirectory + @"\" + FILE_NAME;
      TestContext.WriteLine("Checking file: " + fileName);

      fromCall = fp.FileExists(fileName);

      Assert.IsTrue(fromCall);
    }

    [TestMethod()]
    [Timeout(5000)]
    public void SimulateTimeout()
    {
      System.Threading.Thread.Sleep(4000);
    }

    [TestMethod]
    [Ignore]
    public void FileNameDoesExistSimpleMessage()
    {
      FileProcess fp = new FileProcess();
      bool fromCall;

      fromCall = fp.FileExists(_GoodFileName);

      Assert.IsFalse(fromCall, "File Does NOT Exist.");
    }

    [TestMethod]
    [Ignore]
    public void FileNameDoesExistMessageWithFormatting()
    {
      FileProcess fp = new FileProcess();
      bool fromCall;

      fromCall = fp.FileExists(_GoodFileName);

      Assert.IsFalse(fromCall, "File '{0}' Does NOT Exist.", _GoodFileName);
    }


    [TestMethod]
    [Description("Check to see if a file does NOT exist.")]
    [Owner("Pauls")]
    [Priority(0)]
    [TestCategory("NoException")]
    public void FileNameDoesNotExist()
    {
      FileProcess fp = new FileProcess();
      bool fromCall;

      fromCall = fp.FileExists(BAD_FILE_NAME);

      Assert.IsFalse(fromCall);
    }

    public void SetGoodFileName()
    {
      _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
      if (_GoodFileName.Contains("[AppPath]")) {
        _GoodFileName = _GoodFileName.Replace("[AppPath]",
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
      }
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    [Owner("JohnK")]
    [Priority(1)]
    [TestCategory("Exception")]
    public void FileNameNullOrEmpty_ThrowsArgumentNullException()
    {
      FileProcess fp = new FileProcess();

      fp.FileExists("");
    }


    [TestMethod]
    [Owner("JimR")]
    [Priority(1)]
    [TestCategory("Exception")]
    public void FileNameNullOrEmpty_ThrowsArgumentNullException_UsingTryCatch()
    {
      FileProcess fp = new FileProcess();

      try {
        fp.FileExists("");
      }
      catch (ArgumentNullException) {
        // The test was a success
        return;
      }

      Assert.Fail("Call to FileExists did NOT throw an ArgumentNullException.");
    }
  }
}
