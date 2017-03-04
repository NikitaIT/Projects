using System;

namespace HuffmanLib.Alg
{
    /// <summary>Cтек 1 байт Используется для манипулирования бит-потоком (когда были извлечены или архива)</summary>
    internal struct BitsStack
    {
        /// <summary> Корнтейнер для записи и чтения из потока. </summary>
        public Byte Container;
        private byte Amount;
        public bool IsFull()
        {
            return Amount == 8;
        }
        public bool IsEmpty()
        {
            return Amount == 0;
        }
        public Byte NumOfBits()
        {
            return Amount;
        }
        public void Empty() { Amount = Container = 0; }
        /// <param name="Flag">добавл</param>
        public void PushFlag(bool Flag)
        {
            if (Amount == 8) throw new Exception("Stack is full");
            Container >>= 1;
            if (Flag) Container |= 128;
            ++Amount;
        }
        public bool PopFlag()
        {
            if (Amount == 0) throw new Exception("Stack is empty");
            bool t = (Container & 1) != 0;
            --Amount;
            Container >>= 1;
            return t;
        }
        public void FillStack(Byte Data)
        {
            Container = Data;
            Amount = 8;
        }
    }
}
