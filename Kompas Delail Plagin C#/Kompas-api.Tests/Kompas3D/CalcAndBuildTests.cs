using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApplication1.Kompas3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Error;
using Kompas6API5;

namespace WindowsFormsApplication1.Kompas3D.Tests
{
    [TestClass()]
    public class CalcAndBuildTests
    {
        private ToolStripLabel toolStripLabel;
        private StatusMessage errorMessage;
        private KompasObject _kompasObject;
        private CalcAndBuild calcAndBuild;

        [TestInitialize]
        public void initialize()
        {
            toolStripLabel = new ToolStripLabel();
            errorMessage = new StatusMessage(toolStripLabel);
            _kompasObject = (KompasObject)null;
            calcAndBuild = new CalcAndBuild();
        }

        [TestMethod()]
        public void CalcAndBuildTest()
        {
            calcAndBuild = new CalcAndBuild(41, 100, 60, 20, 5, _kompasObject, errorMessage);
            Assert.IsTrue(calcAndBuild != null);
        }

        [TestMethod()]
        public void CalcAndBuildTestLoad()
        {
            try
            {
                for (int i = 0; i < 10; i++)
                { 
                calcAndBuild = new CalcAndBuild(2, 100, 60, 20, 5, _kompasObject, errorMessage);
                }
            }
            catch (NullReferenceException except)
            {
                return;
            }
            catch (Exception exception)
            {
                Assert.Fail("10 итераций не прошли");
                throw;
            }
            
        }

        [TestMethod()]
        public void CalcTestReturn0()
        {
            uint ret = calcAndBuild.Calc(2, 100, 60, 20, 5);
            Assert.AreEqual((uint)0, ret);
        }
        [TestMethod()]
        public void CalcTestReturn1()
        {
            uint ret = calcAndBuild.Calc(41, 100, 60, 20, 5);
            Assert.AreEqual((uint)1, ret);
        }
        [TestMethod()]
        public void CalcTestReturn2()
        {
            uint ret = calcAndBuild.Calc(1, 2, 3, 4, 5);
            Assert.AreEqual((uint)2, ret);
        }
        [TestMethod()]
        public void CalcTestReturn3()
        {
            uint ret = calcAndBuild.Calc(2, 100, 160, 20, 5);
            Assert.AreEqual((uint)3, ret);
        }
        [TestMethod()]
        public void CalcTestReturn4()
        {
            uint ret = calcAndBuild.Calc(2, 100, 30, 30, 5);
            Assert.AreEqual((uint)4, ret);
        }
        [TestMethod()]
        public void CalcTestReturn5()
        {
            uint ret = calcAndBuild.Calc(2, 100, 30, 20, 5);
            Assert.AreEqual((uint)5, ret);
        }
    }
}