using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyClassesTest
{
  /// <summary>
  /// Assembly Initialize and Cleanup Methods
  /// </summary>
  [TestClass]
  public class MyClassesTestInitialization
  {
   
    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext tc)
    {
      tc.WriteLine("In the Assembly Initialize Method.");
      // TODO: Create resources needed for your tests.
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
      // TODO: Clean up any resources used by your tests.
    }
  }
}
