using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyClassesTest
{
    /// <summary>
    /// Assembly Initialize and cleanup methods
    /// </summary>
    [TestClass]
    public class MyClassesTestInitialization
    {

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext tc)
        {
            tc.WriteLine("In the Assembly Initialize Method.");
            
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {

        }       
    }
}
