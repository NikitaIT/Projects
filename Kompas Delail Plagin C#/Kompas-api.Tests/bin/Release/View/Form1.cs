using System;
using System.Windows.Forms;
using WindowsFormsApplication1.Error;
using WindowsFormsApplication1.Kompas3D;
using Kompas6API5;

namespace WindowsFormsApplication1.View
{
    public partial class Form1 : Form

        // ПЕРЕИМЕНОВАТЬ FORM1
    {
        /// <summary>
        /// Создание объекта KOMПАС-3D
        /// </summary>
        KompasObject _kompas;

        ErrorProvider errorProvider2 = new ErrorProvider();
        ErrorProvider errorProvider3 = new ErrorProvider();
        ErrorProvider errorProvider4 = new ErrorProvider();
        ErrorProvider errorProvider5 = new ErrorProvider();
        StatusMessage message;
        ToolStripLabel errorLabel = new ToolStripLabel();

        public Form1()
        {
            InitializeComponent();
            statusStrip1.Items.Add(errorLabel);
            message = new StatusMessage(errorLabel);


        }

        /// <summary>
        /// Обработка события построения детали, при отсутсвии открытого документа - уведомление пользователя
        /// </summary>
        private void Button1Click(object sender, EventArgs e)
        {
            IsStartBuild();
        }
        /// <summary>
        /// Открыт ли документ
        /// </summary>
        public bool IsDocOpen()
        {
            return (ksDocument3D) _kompas.ActiveDocument3D() != null;
        }
        /// <summary>
        /// Начало сборки
        /// </summary>
        public bool IsStartBuild()
        {
            if (IsDocOpen())
            {
                var depth1 = tValidating(textBoxDepth, errorProvider1);
                var mainDiam1 = tValidating(textBoxMainDiam, errorProvider2);
                var mainDiam21 = tValidating(textBoxMainDiam2, errorProvider3);
                var diam1 = tValidating(textBoxDiam, errorProvider4);
                var holeDiam1 = tValidating(textHoleDiam, errorProvider5);
                if (!(depth1 != 0 & mainDiam1 != 0 & mainDiam21 != 0 & diam1 != 0 & holeDiam1 != 0)) return false;
                var calcAndBuild = new CalcAndBuild(depth1, mainDiam1, mainDiam21, diam1, holeDiam1, _kompas,
                    message);
                return true;
            }
            else
            {
                message.ErrorMessage((uint)StatusMessage.Status.SP);
            }
            return false;
        }
        /// <summary>
        /// Создание обьекта компаса
        /// </summary>
        public void startKompas()
        {
            if (_kompas == null)
            {
                
                try
                {
                    Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                    _kompas = (KompasObject)Activator.CreateInstance(t);
                }
                catch (Exception)
                {
                    _kompas = (KompasObject)null;
                    throw;
                }
                
            }

            if (_kompas != null)
            {
                _kompas.Visible= true;
                _kompas.ActivateControllerAPI();
            }
        }

/// <summary>
/// Обработка событий старта/останова KOMПАС-3D
/// </summary>
private void buttonStartCompas_Click(object sender, EventArgs e)
        {
            if (buttonStartCompas.Text == "Запустить Компас 3D")
            {
                buttonStartCompas.Text = "Остановить Компас 3D";
                groupBox2.Enabled = true;
                startKompas();
            }
            else
            {
                buttonStartCompas.Text = "Запустить Компас 3D";
                groupBox2.Enabled = false;
                try
                {
                    _kompas.Quit();
                    _kompas = null;
                }

                catch
                {
                    message.ErrorMessage((uint)StatusMessage.Status.SZ);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Валидация формы
        /// </summary>
        public ushort tValidating(Control el, ErrorProvider errorProvider)
        {
            ushort val = 0;
            if (string.IsNullOrEmpty(el.Text.Trim()))
            {
                errorProvider.SetError(el, "Обязательное поле!");
            }
            else if (!ushort.TryParse(el.Text.Trim(), out val))
            {
                errorProvider.SetError(el, "Значение некорректно!");
            }
            else if (val==0)
            {
                errorProvider.SetError(el, "Значение не 0!");
            }
            else
            {
                errorProvider.Clear();
            }
            return val;
        }
        private void textBoxDepth_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tValidating(textBoxDepth,errorProvider1);
        }
        private void textBoxMainDiam_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tValidating(textBoxMainDiam, errorProvider2);

        }
        private void textBoxMainDiam2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tValidating(textBoxMainDiam2, errorProvider3);
        }
        private void textBoxDiam_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tValidating(textBoxDiam, errorProvider4);
        }
        private void textHoleDiam_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tValidating(textHoleDiam, errorProvider5);
        }
    }
}
