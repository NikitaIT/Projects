using System;
using System.Windows.Forms;
using HuffmanCode.UI;

namespace HuffmanCode
{
    class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new HuffmanForm();// непосредственный запуск формы (представления)
        }
    }
}
