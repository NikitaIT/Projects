using System;

namespace HuffmanDecode.View
{
    // контракт, по которому представитель будет взаимодействовать с формой
    public interface IHuffmanView : IView
    {
        string In { set; } // назначение одноимённого поля
        string Out { set; } // назначение одноимённого поля
        bool OnOffbtn { set; } // Готовность к кодированию/декодированию активатор кнопок
        string FilePathIn { get; } // получение пути к входному файлу
        string FilePathOut { get; } // получение пути к выходному файлу
        event Action Open;      // событие "файл открыт"
        event Action Decode;    // событие "сообщение раскодированно"
    }
}
