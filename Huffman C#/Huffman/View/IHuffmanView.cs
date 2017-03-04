using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman.View
{
    // контракт, по которому представитель будет взаимодействовать с формой
    public interface IHuffmanView : IView
    {
        string In { set; } // назначение одноимённого поля
        string Out { set; } // назначение одноимённого поля
        string Info { set; } // назначение одноимённого поля
        bool OnOffbtn { set; } // Готовность к кодированию/декодированию активатор кнопок
        string FilePathIn { get; } // получение пути к входному файлу
        string FilePathOut { get; } // получение пути к выходному файлу
        event Action Open;      // событие "файл открыт"
        event Action Code;      // событие "сообщение закодированно"
        event Action Decode;    // событие "сообщение раскодированно"
    }
}
