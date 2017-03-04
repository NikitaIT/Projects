using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Huffman.Model;
using Huffman.Presenter;
using Huffman.View;

namespace Huffman
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

        /// <summary>Заполнение поля отладки</summary>
        public string Info
        {
            set { rtbInfo.Text = value; }
        }

        public bool OnOffbtn {
            set
            {
                btnCode.Enabled = value;
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
        public event Action Code;
        public event Action Decode;

        /// <summary>Конструктор формы</summary>
        public HuffmanForm()
        {
            InitializeComponent();

            /// <summary>Назначаем обработчики</summary>
            btnOpenFileIn.Click += (sender, args) => Invoke(Open);
            btnCode.Click += (sender, args) => Invoke(Code);
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