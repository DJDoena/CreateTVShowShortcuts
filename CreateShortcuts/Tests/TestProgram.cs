using DoenaSoft.CreateShortcuts.Interfaces;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;
using DoenaSoft.CreateShortcuts.Tests.Processors;
using System;

namespace DoenaSoft.CreateShortcuts
{
    public static class TestProgram
    {
        public static void Main(String[] args)
        {
            DefaultsPrinter.PrintDefaults();

            try
            {
                Test(new TestProgram1());

                Test(new TestProgram2());

                Test(new TestProgram3());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                Console.ReadLine();
            }
        }

        private static void Test(IProgram testProgram)
        {
            try
            {
                testProgram.PrintDefaults();
                
                testProgram.Process();
                
                testProgram.Dispose();

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                Console.ReadLine();
            }
        }
    }
}