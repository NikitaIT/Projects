using System;

namespace HuffmanLib.Alg
{
    /// <summary>
    /// Узел HuffmanTree.
    /// Он используется для преобразования байтов в биты при архивировании, биты в байты при извлечении.
    /// </summary>
    internal class TreeNode
    {
        /// <summary>Левый узел</summery>
        public TreeNode Left = null;
        /// <summary>Правый узел</summery>
        public TreeNode Right = null;
        ///<summery>Родительский узел</summery>
        public TreeNode Parent = null;
        /// <summary> Значение частоты узла </summary>
        public ulong Value;
        /// <summary> Значение байт когда узела </summary>
        public Byte ByteValue;

    }

}
