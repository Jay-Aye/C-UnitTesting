using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyClasses;
using System.IO;

namespace MyClassesTest
{
  [TestClass]
  public class FileProcessTestStubs
  {
    [TestMethod]
    public void FileNameDoesExist() {
      Assert.Inconclusive();
    }

    [TestMethod]
    public void FileNameDoesNotExist() {
      Assert.Inconclusive();
    }

    [TestMethod]
    public void FileNameNullOrEmpty_ThrowsArgumentNullException() {
      Assert.Inconclusive();
    }
  }
}
