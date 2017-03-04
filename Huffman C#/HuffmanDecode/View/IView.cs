namespace HuffmanDecode.View
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
