using System;
using System.IO;

namespace HuffmanLib.Alg
{
    public partial class HuffmanAlgorithm : IDisposable
    {
        /// <summary> строем дерево и тд. </summary>
        /// <param name="Data"> поток для записи</param>
        public Stream Encode(Stream Data)
        {
            String TempDir = Environment.GetEnvironmentVariable("temp");
            HuffmanTree huffmanTree = new HuffmanTree(BuildFrequencyTable(Data));
            FileStream tempFS = new FileStream(TempDir + @"\TempArch.tmp", FileMode.Create);
            WriteHeader(tempFS, huffmanTree.frequencyTable, Data.Length, 11, GetComplementsBits(huffmanTree));
            long DataSize = Data.Length;
            TreeNode TempNode = null;
            Byte Original;

            short j;
            int k;
            for (long i = 0; i < DataSize; ++i)
            {
                Original = (Byte) Data.ReadByte();
                TempNode = huffmanTree.Leafs[ByteLocation[Original]];
                while (TempNode.Parent != null)
                {
                    BitsList.Add(TempNode.Parent.Left == TempNode);
                    TempNode = TempNode.Parent;
                }
                BitsList.Reverse();
                k = BitsList.Count;
                for (j = 0; j < k; ++j)
                {
                    Stack.PushFlag((bool) BitsList[j]);
                    if (Stack.IsFull())
                    {
                        tempFS.WriteByte(Stack.Container);
                        Stack.Empty();
                    }
                }
                BitsList.Clear();
            }

            //Запись последнего байта, если стек не пуст.
            if (!Stack.IsEmpty())
            {
                Byte BitsToComplete = (Byte) (8 - Stack.NumOfBits());
                for (byte Count = 0; Count < BitsToComplete; ++Count)
                    Stack.PushFlag(false);
                tempFS.WriteByte(Stack.Container);
                Stack.Empty();
            }

            tempFS.Seek(0, SeekOrigin.Begin);
            return tempFS;
        }

        /// <summary>архивнуть </summary>
        public void Encode(Stream Data, string OutputFile)
        {
            HuffmanTree huffmanTree = new HuffmanTree(BuildFrequencyTable(Data));
            FileStream tempFS = new FileStream(OutputFile, FileMode.Create);
            WriteHeader(tempFS, huffmanTree.frequencyTable, Data.Length, 11, GetComplementsBits(huffmanTree));
            long DataSize = Data.Length;
            TreeNode TempNode = null;
            Byte Original;

            short j;
            int k;
            for (long i = 0; i < DataSize; ++i)
            {
                Original = (Byte) Data.ReadByte();
                TempNode = huffmanTree.Leafs[ByteLocation[Original]];
                while (TempNode.Parent != null)
                {

                    BitsList.Add(TempNode.Parent.Left == TempNode);
                    TempNode = TempNode.Parent;
                }
                BitsList.Reverse();
                k = BitsList.Count;
                for (j = 0; j < k; ++j)
                {
                    Stack.PushFlag((bool) BitsList[j]);
                    if (Stack.IsFull())
                    {
                        tempFS.WriteByte(Stack.Container);
                        Stack.Empty();
                    }
                }
                BitsList.Clear();
            }
            if (!Stack.IsEmpty())
            {
                Byte BitsToComplete = (Byte) (8 - Stack.NumOfBits());
                for (byte Count = 0; Count < BitsToComplete; ++Count)
                    Stack.PushFlag(false);
                tempFS.WriteByte(Stack.Container);
                Stack.Empty();
            }
            tempFS.Seek(0, SeekOrigin.Begin);
            tempFS.Close();
        }
    }
}
