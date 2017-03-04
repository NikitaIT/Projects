using System;
using WindowsFormsApplication1.View;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kompas_api.Tests
{
    [TestClass]
    public class TestLoad
    {
        [TestMethod()]
        public void ApplicationTestLoad()
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    System.Windows.Forms.Application.Run(new CompasModuleForm());
                }
            }
            catch (Exception)
            {
                Assert.Fail("10 итераций не прошли");
                throw;
            }
            
        }
    }
}
