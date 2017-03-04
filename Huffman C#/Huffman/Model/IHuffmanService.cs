using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman.Model
{
    /// <summary>Сервис построения формы для хафмана</summary>
    public interface IHuffmanService
    {
        /// <summary>Проверка готовности файла к чтению-записи, true - готов </summary>
        /// <param name="filePath">Путь к файлу</param>
        bool IsSourceAndOutputOK(string filePath);
    }
}
