using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApplication1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kompas6API5;

namespace WindowsFormsApplication1.View.Tests
{
    [TestClass()]
    public class Form1Tests
    {
        private CompasModuleForm _compasModuleForm;
        private Control control;
        private ErrorProvider errorProvider;

        [TestInitialize()]
        public void form1TestsInitialize()
        {
            _compasModuleForm = new CompasModuleForm();
            control = new TextBox();
            errorProvider = new ErrorProvider();
            Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
        }

        [TestMethod()]
        public void DoValidateTestByUshort()
        {
            Control control = new TextBox();
            ErrorProvider errorProvider = new ErrorProvider();
            for (ushort val = 1; val < ushort.MaxValue; val++)
            {
                control.Text = val.ToString();
                var ret = _compasModuleForm.DoValidate(control, errorProvider);
                Assert.AreEqual(val, ret, "От 0 до ushort.MaxValue не возвращает переданное, а возвр: " + val);
            }
        }
        [TestMethod()]
        public void DoValidateTestByPlasNull()
        {
            Control control = new TextBox();
            ErrorProvider errorProvider = new ErrorProvider();
            for (int val = (int)ushort.MaxValue + 1; val < ushort.MaxValue + 10000; val++)
            {
                control.Text = val.ToString();
                var ret = _compasModuleForm.DoValidate(control, errorProvider);
                Assert.AreEqual(0, ret, "Ожидался 0 на значении: " + val);
            }
        }
        [TestMethod()]
        public void DoValidateTestByMinusNull()
        {

            for (int val = -ushort.MaxValue; val < 0; val++)
            {
                control.Text = val.ToString();
                var ret = _compasModuleForm.DoValidate(control, errorProvider);
                Assert.AreEqual(0, ret, "Ожидался 0 на значении: " + val);
            }
        }
        
        [TestMethod()]
        public void DoValidateTestNAN()
        {
            {
                var val = "SD";
                control.Text = val.ToString();
                var ret = _compasModuleForm.DoValidate(control, errorProvider);
                Assert.AreEqual(0, ret, "Ожидался 0 на значении: " + val);
            }
            {
                var val = "S2D";
                control.Text = val.ToString();
                var ret = _compasModuleForm.DoValidate(control, errorProvider);
                Assert.AreEqual(0, ret, "Ожидался 0 на значении: " + val);
            }
            {
                var val = "2 r";
                control.Text = val.ToString();
                var ret = _compasModuleForm.DoValidate(control, errorProvider);
                Assert.AreEqual(0, ret, "Ожидался 0 на значении: " + val);
            }
            {
                var val = "e 1";
                control.Text = val.ToString();
                var ret = _compasModuleForm.DoValidate(control, errorProvider);
                Assert.AreEqual(0, ret, "Ожидался 0 на значении: " + val);
            }
            {
                var val = "04 32 13 04 123";
                control.Text = val.ToString();
                var ret = _compasModuleForm.DoValidate(control, errorProvider);
                Assert.AreEqual(0, ret, "Ожидался 0 на значении: " + val);
            }
        }
    }
}