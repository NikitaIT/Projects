using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman.View
{
    /// <summary>Интерфейс для показа/сокрытия форм</summary>
    public interface IView
    {
        /// <summary>Показать представление</summary>
        void Show();
        /// <summary>Закрыть представление</summary>
        void Close();
    }
}
