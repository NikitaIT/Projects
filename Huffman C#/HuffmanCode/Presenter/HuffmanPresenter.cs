using System;
using System.IO;
using System.Windows.Forms;
using HuffmanCode.Model;
using HuffmanCode.View;
using HuffmanLib.Alg;

namespace HuffmanCode.Presenter
{
    /// <summary>Представитель главной формы, содержит всю логику</summary>
    public class HuffmanPresenter : IPresenter
    {
        /// <summary>Объявление зависимостей</summary>
        private readonly IHuffmanView _view;

        private readonly IHuffmanService _service;

        /// <summary>Объявление внутренних полей</summary>
        private readonly HuffmanAlgorithm _huffmanAlgorithm;

        private FileStream _fileStream;

        /// <summary>Конструктор</summary>
        /// <param name="view">Элемент представления для формы</param>
        /// <param name="service">Обработчик действий с моделью</param>
        public HuffmanPresenter(IHuffmanView view, IHuffmanService service)
        {
            _view = view;
            _service = service;

            /// <summary>Инициализируем пустые поля</summary>
            _huffmanAlgorithm = new HuffmanAlgorithm();
            _fileStream = null;

            /// <summary>Прикрепляем обработчики</summary>
            _view.Open += () => Open(_view.FilePathIn);
            _view.Code += () => Code(_view.FilePathOut);
        }

        /// <summary>Открытие входного файла</summary>
        /// <param name="filePath">Путь к входному файлу</param>
        private void Open(string filePath)
        {
            if (!_service.IsSourceAndOutputOK(filePath)) return;
            _fileStream?.Close();
            _fileStream = new FileStream(filePath, FileMode.Open);
            UpdateIn();
            UpdateCodeAndDecodeBtn(true);
        }

        /// <summary>Поазать форму</summary>
        public void Run()
        {
            _view.Show();
        }

        /// <summary>Кодирование ранее открытого файла</summary>
        /// <param name="filePath">Путь к выходному файлу</param>
        private void Code(string filePath)
        {
            try
            {
                /// <summary>Кодирование файла</summary>
                _huffmanAlgorithm.Encode(_fileStream, filePath);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка содержимого файла", "Входной файл повреждён", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /// <summary>Закрытие кодируемого файла</summary>
            _fileStream.Close();

            /// <summary>Обновление полей</summary>
            UpdateIn();
            UpdateOut(filePath);
            UpdateCodeAndDecodeBtn(false);
        }

        /// <summary>Обновление состояния кнопок</summary>
        private void UpdateCodeAndDecodeBtn(bool val)
        {
            _view.OnOffbtn = val;
        }
        /// <summary>Обновление имени входного файла</summary>
        private void UpdateIn()
        {
            _view.In = _fileStream.Name??"";
        }

        /// <summary>Обновление имени выходного файла</summary>
        private void UpdateOut(string filePath)
        {
            _view.Out = filePath;
        }
    }
}
