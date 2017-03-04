using System;

namespace HuffmanLib.Alg
{
    /// <summary>Заголовок</summary>
    [Serializable]
    internal class FileHeader
    {

        /// <summary>Версия.</summary>
        public readonly byte version;

        /// <summary>Частоты.</summary>
        public readonly FrequencyTable frequencyTable;

        /// <summary>Разарх длин.</summary>
        public readonly long OriginalSize;

        /// <summary>Количество дополнительных битов, добавленных к последним байтом данных.</summary>
        public readonly byte ComplementsBits;

        /// <summary>конструктор </summary>
        public FileHeader(Byte ver, FrequencyTable T, ref long OrgSize,
            byte BitsToFill)
        {
            version = ver; frequencyTable = T; OriginalSize = OrgSize; ComplementsBits = BitsToFill;
        }
    }

}
