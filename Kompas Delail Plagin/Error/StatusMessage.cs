using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
namespace WindowsFormsApplication1.Error
{

    /// <summary>
    ///     Сообщения об ошибках и предупреждения пользователя
    /// </summary>
    public class StatusMessage
    {
        private string[] errArray;
        private string[] infoArray;
        private ToolStripLabel control;
        public enum Status
        {
            //logic valid. status
            NotFound,
            NotValidDiskThickness,
            NotValidDiscDiameter,
            NotValidProtrusionDiameter,
            NotValidPlantDiameter,
            BigFixtureDiameter,
            SmallFixtureDiameter,
            //logic form status
            NotSetDocument,
            NotRunCompass,
        }

        /// <summary>
        ///     Инициализация статус-надписей
        /// </summary>
        /// <param name="control">Подпись для вывода текста</param>
        public StatusMessage(ToolStripLabel control)
        {
            this.control = control;
            this.control.Text = "".ToString();

            #region Ошибки текстом
            errArray = new string[]
            {
                "Успешно запущено",//0

                "Толщина диска от 1 до 40!",//1
                "Диаметр диска от 50 до 400!",//2
                "Диаметр выступа не должен превышать 80% диаметра диска!",//3
                "Диаметр посадочного отверстия не должен превышать 80% диаметра выступа!",//4
                "Диаметр крепежных отверстий слишком большой!",//5
                "Диаметр крепежных отверстий слишком мал!",//6   
                       
                "Создайте пустой документ!",//7
                "Ошибка, нужно снова запустить!",//8
            };
            #endregion

        }

        /// <summary>
        ///     Выдает сообщение ошибку
        /// </summary>
        /// <param name="statusCode">Код ошибки</param>
        public void ErrorMessage(uint statusCode)
        {
            if (statusCode == 0)
            {
                InfoMessage(statusCode);
                return;
            }

            #region Вывод в меседжбокс
            if (statusCode <= 6)
            {
                MessageBox.Show((statusCode < errArray.Length ? errArray[statusCode] : "Неизвестная ошибка"), "Неверный ввод", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (statusCode > 6)
            {
                MessageBox.Show((statusCode < errArray.Length ? errArray[statusCode] : "Неизвестная ошибка"), "Системная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region Вывод в статусбар
            /*
            if (statusCode <= 6)
            {
                control.Text = "Неверный ввод: " + (statusCode < errArray.Length ? errArray[statusCode] : "Неизвестная ошибка");
            }
            if (statusCode > 6)
            {
                control.Text = "Системная ошибка: " + (statusCode < errArray.Length ? errArray[statusCode] : "Неизвестная ошибка");
            }
            */
            #endregion

        }
        /// <summary>
        ///     Выдает информационное сообщение
        /// </summary>
        /// <param name="statusCode">Код ошибки</param>
        public void InfoMessage(uint statusCode)
        {
            //control.Text = "Инфо: " + (statusCode < errArray.Length ? errArray[statusCode] : "Неизвестное сообщение");
            MessageBox.Show((statusCode < errArray.Length ? errArray[statusCode] : "Неизвестное сообщение"), "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
