using System;
using System.Windows.Forms;
using WindowsFormsApplication1.Error;
using WindowsFormsApplication1.Kompas3D;
using Kompas6API5;

namespace WindowsFormsApplication1.View
{
    public partial class CompasModuleForm : Form
    {
        /// <summary>
        ///  Объект KOMПАС-3D
        /// </summary>
        KompasObject _kompas;

        /// <summary>
        ///  Обработчики ошибок для форм
        /// </summary>
        ErrorProvider errorProvider2 = new ErrorProvider();
        ErrorProvider errorProvider3 = new ErrorProvider();
        ErrorProvider errorProvider4 = new ErrorProvider();
        ErrorProvider errorProvider5 = new ErrorProvider();

        /// <summary>
        /// Обработчик сообщений
        /// </summary>
        StatusMessage message;
        /// <summary>
        /// Подпись ошибки для статусбара
        /// </summary>
        ToolStripLabel errorLabel = new ToolStripLabel();

        public CompasModuleForm()
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
        private bool IsDocOpen()
        {
            return (ksDocument3D) _kompas.ActiveDocument3D() != null;
        }
        /// <summary>
        /// Начало сборки
        /// </summary>
        private bool IsStartBuild()
        {
            if (IsDocOpen())
            {
                var depth = DoValidate(textBoxDepth, errorProvider1);
                var mainDiam = DoValidate(textBoxMainDiam, errorProvider2);
                var mainDiam2 = DoValidate(textBoxMainDiam2, errorProvider3);
                var diam = DoValidate(textBoxDiam, errorProvider4);
                var holeDiam = DoValidate(textHoleDiam, errorProvider5);
                if (!(depth != 0 & mainDiam != 0 & mainDiam2 != 0 & diam != 0 & holeDiam != 0)) return false;
                var calcAndBuild = new CalcAndBuild(depth, mainDiam, mainDiam2, diam, holeDiam, _kompas,
                    message);
                return true;
            }
            else
            {
                message.ErrorMessage((uint)StatusMessage.Status.NotSetDocument);
            }
            return false;
        }
        /// <summary>
        /// Создание обьекта компаса
        /// </summary>
        private void StartKompas()
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
                StartKompas();
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
                    message.ErrorMessage((uint)StatusMessage.Status.NotRunCompass);
                }
            }
        }

        /// <summary>
        /// Установка обработчика валидации
        /// </summary>
        private void textBoxDepth_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidate(textBoxDepth,errorProvider1);
        }
        
        /// <summary>
        /// Установка обработчика валидации
        /// </summary>
        private void textBoxMainDiam_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidate(textBoxMainDiam, errorProvider2);

        }
        
        /// <summary>
        /// Установка обработчика валидации
        /// </summary>
        private void textBoxMainDiam2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidate(textBoxMainDiam2, errorProvider3);
        }
        
        /// <summary>
        /// Установка обработчика валидации
        /// </summary>
        private void textBoxDiam_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidate(textBoxDiam, errorProvider4);
        }

        /// <summary>
        /// Установка обработчика валидации
        /// </summary>
        private void textHoleDiam_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DoValidate(textHoleDiam, errorProvider5);
        }

        /// <summary>
        /// Валидация формы
        /// </summary>
        /// <param name="el">Элемент-отправитель события</param>
        /// <param name="errorProvider">Обработчик ошибок</param>
        public ushort DoValidate(Control el, ErrorProvider errorProvider)
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
            else if (val == 0)
            {
                errorProvider.SetError(el, "Значение не 0!");
            }
            else
            {
                errorProvider.Clear();
            }
            return val;
        }
    }
}
