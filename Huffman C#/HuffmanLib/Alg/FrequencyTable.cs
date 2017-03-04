using System;

namespace HuffmanLib.Alg
{
    /// <summary>
    /// Строим из байтов и их повторений в потоке. исп 2 массива одноразмерных
    /// </summary>
    [Serializable]
    internal class FrequencyTable
    {
        /// <summary>
        ///  Сохраняет все типы байтов(до 256), найденные в потоке.
        /// </summary>
        public Byte[] FoundBytes;
        /// <summary>
        /// Сохраняет количество появлений каждого байта.
        /// </summary>
        public uint[] Frequency;
    }
}
