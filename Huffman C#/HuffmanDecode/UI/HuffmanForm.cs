using System;
using System.IO;
using System.Windows.Forms;
using HuffmanDecode.Model;
using HuffmanDecode.Presenter;
using HuffmanDecode.View;

namespace HuffmanDecode.UI
{

    public partial class HuffmanForm : Form, IHuffmanView
    {
        HuffmanPresenter _presenter;

        /// <summary>Заполнение поля считанного файла</summary>
        public string In
        {
            set { rtbIn.Text = value; }
        }

        /// <summary>Заполнение поля результата</summary>
        public string Out
        {
            set { rtbOut.Text = value; }
        }

        public bool OnOffbtn {
            set
            {
                btnDecode.Enabled = value;
            }
        }

        /// <summary>Получение пути к обрабатываемому файлу</summary>
        public string FilePathIn
        {
            get
            {
                openFileDialogIn.ShowDialog();
                return new FileInfo(openFileDialogIn.FileName).FullName;
            }
        }

        /// <summary>Получение пути к выходному файлу</summary>
        public string FilePathOut
        {
            get
            {
                saveFileDialogOut.ShowDialog();
                return new FileInfo(saveFileDialogOut.FileName).FullName;
            }
        }

        /// <summary>Отображение формы</summary>
        public new void Show()
        {
            Application.Run(this);
        }

        /// <summary>Список возможных сигналов</summary>
        public event Action Open;
        public event Action Decode;

        /// <summary>Конструктор формы</summary>
        public HuffmanForm()
        {
            InitializeComponent();

            /// <summary>Назначаем обработчики</summary>
            btnOpenFileIn.Click += (sender, args) => Invoke(Open);
            btnDecode.Click += (sender, args) => Invoke(Decode);

            /// <summary>Настраиваем форму для открытия</summary>
            openFileDialogIn.CheckPathExists = false;
            openFileDialogIn.Multiselect = false;

            /// <summary>Настраиваем форму для сохранения</summary>
            saveFileDialogOut.CheckPathExists = false;

            /// <summary>Создаем и запускаем презентера</summary>
            IHuffmanService service = new HuffmanService();
            _presenter = new HuffmanPresenter(this, service);
            _presenter.Run();

        }

        /// <summary>Посылаем сигнал</summary>
        /// <param name="action">Сигнал</param>
        private void Invoke(Action action)
        {
            if (action != null) action();
        }
    }

}