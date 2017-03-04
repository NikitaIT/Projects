using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HuffmanLib.Alg
{

    /// <summary>
    /// Реализация алгоритма Хаффмана.
    /// </summary>
    public partial class HuffmanAlgorithm : IDisposable
    {
        private Byte[] ByteLocation = new Byte[256];
        private bool[] IsByteExist;

        /// <summary>байты где найдены</summary>
        private ArrayList BytesList = new ArrayList();

        /// <summary>количество повторений байта</summary>
        private ArrayList AmountList = new ArrayList();

        /// <summary>обратный путь к битам</summary>
        private ArrayList BitsList = new ArrayList();

        /// <summary>хедер писать и читать</summary>
        private BinaryFormatter BinFormat = new BinaryFormatter();

        /// <summary>для записи при декодир и кодир</summary>
        private BitsStack Stack = new BitsStack();

       /// <summary> Проверяет, является ли поток данных в архивом.</summary>
        public bool IsArchivedStream(Stream Data)
        {
            Data.Seek(0, SeekOrigin.Begin);
            bool test = true;
            try
            {
                FileHeader Header = (FileHeader)BinFormat.Deserialize(Data);
                Header = null;
            }
            catch (Exception)
            {
                test = false;
            }
            finally
            {
                Data.Seek(0, SeekOrigin.Begin);
            }
            return test;
        }

        /// <summary>Отношение к хранененном в архиве данного потока от заархивированного.</summary>
        /// <param name="Data"> для расчета коэффициента архивирования </returns>
        public float GetArchivingRatio(Stream Data)
        {
            Data.Seek(0, SeekOrigin.Begin);
            float Result;
            try
            {
                FileHeader Header = (FileHeader)BinFormat.Deserialize(Data);
                Result = (100f * Data.Length) / Header.OriginalSize;
                Header = null;
            }
            catch (Exception)
            {
                throw new Exception("Error, the given stream isn't Huffman archived or corrupted.");
            }
            finally
            {
                Data.Seek(0, SeekOrigin.Begin);
            }
            return Result;
        }
        public void Dispose()
        {
            BytesList = null;
            IsByteExist = null;
            AmountList = null;
            BinFormat = null;
            BitsList = null;
            ByteLocation = null;
        }
        /// <summary> Сканирование на наличие повторяющихся байтов и в соответствии с ними построение таблицы частот. </summary>
        /// <param name="DataSource">Поток бит.</param>
        private FrequencyTable BuildFrequencyTable(Stream DataSource)
        {
            long OriginalPosition = DataSource.Position;
            FrequencyTable frequencyTable = new FrequencyTable();
            IsByteExist = new bool[256];

            Byte bTemp;
            // Подсчет байтов и сохранение их
            for (long i = 0; i < DataSource.Length; ++i)
            {
                bTemp = (Byte)DataSource.ReadByte();
                if (IsByteExist[bTemp]) //Если байт был найден, иначе новый
                    AmountList[ByteLocation[bTemp]] = (uint)AmountList[ByteLocation[bTemp]] + 1;
                else
                {
                    IsByteExist[bTemp] = true; //найден
                    ByteLocation[bTemp] = (Byte)BytesList.Count;
                    AmountList.Add(1u);
                    BytesList.Add(bTemp);
                }
            }
            int ArraySize = BytesList.Count;
            frequencyTable.FoundBytes = new byte[ArraySize];
            frequencyTable.Frequency = new uint[ArraySize];
            short ArraysSize = (short)ArraySize;
            for (short i = 0; i < ArraysSize; ++i)
            {
                frequencyTable.FoundBytes[i] = (Byte)BytesList[i];
                frequencyTable.Frequency[i] = (uint)AmountList[i];
            }
            SortArrays(frequencyTable.Frequency, frequencyTable.FoundBytes, ArraysSize);

            //очистка ресурсов
            IsByteExist = null;
            BytesList.Clear();
            AmountList.Clear();
            DataSource.Seek(OriginalPosition, SeekOrigin.Begin);
            return frequencyTable;
        }
        /// <summary> сортировка</summary>
        private void SortArrays(uint[] SortTarget, Byte[] TweenArray, short size)
        {
            --size;
            bool TestSwitch = false;
            Byte BTemp;
            uint uiTemp;
            short i, j;
            for (i = 0; i < size; ++i)
            {
                for (j = 0; j < size; ++j)
                {
                    if (SortTarget[j] >= SortTarget[j + 1]) continue;
                    TestSwitch = true;
                    uiTemp = SortTarget[j];
                    SortTarget[j] = SortTarget[j + 1];
                    SortTarget[j + 1] = uiTemp;
                    BTemp = TweenArray[j];
                    TweenArray[j] = TweenArray[j + 1];
                    TweenArray[j + 1] = BTemp;
                }
                if (!TestSwitch) break;
                TestSwitch = false;
            }
            for (i = 0; i < SortTarget.Length; ++i)
                ByteLocation[TweenArray[i]] = (Byte)i;

        }
        /// <summary> записать шапук </summary>
        private void WriteHeader(Stream St, FrequencyTable frequencyTable, long OriginalSize,
            Byte version, Byte ComplementsBits)
        {
            FileHeader Header = new FileHeader(version, frequencyTable, ref OriginalSize,
                ComplementsBits);
            BinFormat.Serialize(St, Header);
        }
        /// <summary> Рассчитывает количество комплементов битов, необходимое для последнего байта записи. </summary>
        /// <param name="huffmanTree">Дерево</param>
        private Byte GetComplementsBits(HuffmanTree huffmanTree)
        {
            //Getting the deapth of each leaf in the huffman tree
            short i = (short)huffmanTree.Leafs.Length;
            ushort[] NodesDeapth = new ushort[i];
            long SizeInOfBits = 0;
            while (--i != -1)
            {
                TreeNode TN = huffmanTree.Leafs[i];
                while (TN.Parent != null)
                {
                    TN = TN.Parent;
                    ++NodesDeapth[i];
                }
                SizeInOfBits += NodesDeapth[i] * huffmanTree.frequencyTable.Frequency[i];
            }
            return (byte)(8 - SizeInOfBits % 8);
        }
     
    }
}
