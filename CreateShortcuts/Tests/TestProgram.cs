using System;
using System.Diagnostics;
using DoenaSoft.CreateShortcuts.Interfaces;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;
using DoenaSoft.CreateShortcuts.Tests.Processors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoenaSoft.CreateShortcuts
{
    [TestClass]
    public class TestProgram
    {
        [TestMethod]
        public void TestProgram1()
        {
            Test(new TestProgram1());
        }

        [TestMethod]
        public void TestProgram2()
        {
            Test(new TestProgram2());
        }

        [TestMethod]
        public void TestProgram3()
        {
            Test(new TestProgram3());
        }

        private static void Test(IProgram testProgram)
        {
            try
            {
                DefaultsPrinter.PrintDefaults();

                testProgram.PrintDefaults();

                testProgram.Process();

                testProgram.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);

                throw;
            }
        }
    }
}