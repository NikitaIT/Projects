using System;
using System.IO;

namespace HuffmanLib.Alg
{
    /// <summary>
    /// Реализация алгоритма Хаффмана.
    /// </summary>
    public partial class HuffmanAlgorithm : IDisposable
    {
        public Stream Decode(Stream Data)
        {
            Data.Seek(0, SeekOrigin.Begin);
            //Tempdirectory
            String TempDir = Environment.GetEnvironmentVariable("temp");
            FileHeader Header;
            //Reading the header data from the stream

            if (!IsArchivedStream(Data)) throw new Exception("The given stream is't Huffmans algorithm archive type.");
            Header = (FileHeader) BinFormat.Deserialize(Data);

            //Gernerating Huffman tree out of the frequency table in the header
            HuffmanTree huffmanTree = new HuffmanTree(Header.frequencyTable);
            //Creating temporary file
            FileStream tempFS = new FileStream(TempDir + @"\TempArch.tmp", FileMode.Create);
            BitsStack Stack = new BitsStack();
            long DataSize = Data.Length - Data.Position;
            if (Header.ComplementsBits == 0) DataSize += 1;
            TreeNode TempNode = null;

            while (true)
            {
                TempNode = huffmanTree.RootNode;

                //As long it's not a leaf, go down the tree
                while (TempNode.Left != null && TempNode.Right != null)
                {
                    //If the stack is empty refill it.
                    if (Stack.IsEmpty())
                    {
                        Stack.FillStack((Byte) Data.ReadByte());
                        if ((--DataSize) == 0)
                        {
                            goto AlmostDone;
                        }
                    }
                    //Going left or right according to the bit
                    TempNode = Stack.PopFlag() ? TempNode.Left : TempNode.Right;
                }
                //By now reached for a leaf and writes it's data.
                tempFS.WriteByte(TempNode.ByteValue);
            } //end of while

            //To this lable u can jump only from the while loop (only one byte left).
            AlmostDone:

            short BitsLeft = (Byte) (Stack.NumOfBits() - Header.ComplementsBits);

            //Writing the rest of the last byte.
            if (BitsLeft != 8)
            {
                bool Test = TempNode.Left == null && TempNode.Right == null;
                while (BitsLeft > 0)
                {
                    //If at itteration, TempNode not done going down the huffman tree.
                    if (Test) TempNode = huffmanTree.RootNode;
                    while (TempNode.Left != null && TempNode.Right != null)
                    {
                        //Going left or right according to the bit
                        TempNode = Stack.PopFlag() ? TempNode.Left : TempNode.Right;
                        --BitsLeft;
                    }
                    //By now reached for a leaf and writes it's data.
                    tempFS.WriteByte(TempNode.ByteValue);
                    Test = true;
                }
            }
            tempFS.Seek(0, SeekOrigin.Begin);
            return tempFS;
        }

        public bool Decode(Stream Data, string OutputFile)
        {
            Data.Seek(0, SeekOrigin.Begin);
            FileHeader Header;

            if (!IsArchivedStream(Data)) throw new Exception("The given stream is't my Huffman algorithm type.");
            Header = (FileHeader) BinFormat.Deserialize(Data);

            HuffmanTree huffmanTree = new HuffmanTree(Header.frequencyTable);
            FileStream tempFS = new FileStream(OutputFile, FileMode.Create);
            BitsStack Stack = new BitsStack();
            long DataSize = Data.Length - Data.Position;
            if (Header.ComplementsBits == 0) DataSize += 1;
            TreeNode TempNode = null;

            while (true)
            {
                TempNode = huffmanTree.RootNode;

                //Пока это не лист, идти вниз по дереву
                while (TempNode.Left != null && TempNode.Right != null)
                {
                    //Если стек пуст пополнить его.
                    if (Stack.IsEmpty())
                    {
                        Stack.FillStack((Byte) Data.ReadByte());
                        if ((--DataSize) == 0)
                        {
                            goto AlmostDone;
                        }
                    }
                    //влево или вправо в соответствии с битом
                    TempNode = Stack.PopFlag() ? TempNode.Left : TempNode.Right;
                }
                tempFS.WriteByte(TempNode.ByteValue);
            }
            AlmostDone:
            short BitsLeft = (Byte) (Stack.NumOfBits() - Header.ComplementsBits);

            // отдать остальную часть последнего байта.
            if (BitsLeft != 8)
            {
                bool Test = TempNode.Left == null && TempNode.Right == null;
                while (BitsLeft > 0)
                {
                    // Если в itteration, TempNode не сделали спускаясь дерево Хаффмана.
                    if (Test) TempNode = huffmanTree.RootNode;
                    while (TempNode.Left != null && TempNode.Right != null)
                    {
                        //влево или вправо в соответствии с битом
                        TempNode = Stack.PopFlag() ? TempNode.Left : TempNode.Right;
                        --BitsLeft;
                    }
                    // к листу и записать данные.
                    tempFS.WriteByte(TempNode.ByteValue);
                    Test = true;
                }
            }

            tempFS.Close();
            return true;
        }
    }
}
