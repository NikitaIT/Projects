namespace HuffmanDecode.Model
{
    /// <summary>Сервис построения формы для хафмана</summary>
    public interface IHuffmanService
    {
        /// <summary>Проверка готовности файла к чтению-записи, true - готов </summary>
        /// <param name="filePath">Путь к файлу</param>
        bool IsSourceAndOutputOK(string filePath);
    }
}
