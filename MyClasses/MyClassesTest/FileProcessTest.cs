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
        public static void ClassCleanup(TestContext tc)
        {
            tc.WriteLine("In the Class Cleanup.");
        }
        #endregion

        #region Test Initialize and Cleanup
        [TestInitialize]
        public void TestInitialize()
        {
            if (TestContext.TestName == "FileNameDoesExist")
            {
                SetGoodFileName();
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine("Creating File: " + _GoodFileName);
                    File.AppendAllText(_GoodFileName, "Some Text");

                }
            }
        }

        #endregion

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void FileNameDoesExist()
        {
            FileProcess fp = new FileProcess();
            bool fromcall;

            SetGoodFileName();
            TestContext.WriteLine("Creating the file: " + _GoodFileName);
            File.AppendAllText(_GoodFileName, "Some Text");
            TestContext.WriteLine("Testing the file: " + _GoodFileName);
            fromcall = fp.FileExists(_GoodFileName);
            TestContext.WriteLine("Deleting the file: " + _GoodFileName);
            File.Delete(_GoodFileName);

            Assert.IsTrue(fromcall);
            
        }

        [TestMethod]
        public void FileNameDoesNotExist()
        {
            FileProcess fp = new FileProcess();
            bool fromcall;

            fromcall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromcall);
        }

        [TestMethod]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            FileProcess fp = new FileProcess();
            
            try
            {
                fp.FileExists("");
            }
            catch (ArgumentNullException)
            {
                return;
            }

            Assert.Fail("Call to FileExists did not throw an ArumentNullException");
        }
        public void SetGoodFileName()
        {
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }
    }
}
