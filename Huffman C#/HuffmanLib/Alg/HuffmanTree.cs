using System;
using System.Collections;

namespace HuffmanLib.Alg
{
    /// <summary>
    /// Реализация алгоритма Хаффмана.
    /// </summary>
    public partial class HuffmanAlgorithm : IDisposable
    {
        /// <summary>Дерево </summary>
        internal class HuffmanTree
        {
            /// <summary>Значения таблицы частот.</summary>
            public readonly TreeNode[] Leafs;

            /// <summary> Таблица частот для построения дерева Хаффмана </summary>
            public readonly FrequencyTable frequencyTable;

            /// <summary>Узлы без родителей(сироты) </summary>
            private ArrayList OrphanNodes = new ArrayList();

            /// <summary>Корень</summary>
            public readonly TreeNode RootNode;

            /// <summary>Построение дерева Хаффмана из таблицы частот.</summary>
            internal HuffmanTree(FrequencyTable frequencyTable)
            {
                ushort Length = (ushort) frequencyTable.FoundBytes.Length;
                this.frequencyTable = frequencyTable;
                Leafs = new TreeNode[Length];
                if (Length > 1) // один корень
                {
                    for (ushort i = 0; i < Length; ++i)
                    {
                        Leafs[i] = new TreeNode
                        {
                            ByteValue = frequencyTable.FoundBytes[i],
                            Value = frequencyTable.Frequency[i]
                        };
                    }
                    OrphanNodes.AddRange(Leafs);
                    RootNode = BuildTree();
                }
                else
                {
                    var TempNode = new TreeNode();
                    TempNode.ByteValue = frequencyTable.FoundBytes[0];
                    TempNode.Value = frequencyTable.Frequency[0];
                    RootNode = new TreeNode();
                    RootNode.Left = RootNode.Right = TempNode;
                }
                OrphanNodes.Clear();
                OrphanNodes = null;
            }

            /// <summary> функция построения дерева из таблицы частот</summary>
            /// <returns>Корень</returns>
            private TreeNode BuildTree()
            {
                TreeNode small, smaller, newParent = null;
                /// <summary> пока не построили</summary>
                while (OrphanNodes.Count > 1)
                {
                    FindSmallestOrphanNodes(out smaller, out small);
                    newParent = new TreeNode
                    {
                        Value = small.Value + smaller.Value,
                        Left = smaller,
                        Right = small
                    };
                    smaller.Parent = small.Parent = newParent;
                    OrphanNodes.Add(newParent);
                }
                return newParent;
            }

            /// <summary>
            /// Удаление двух наименьших сиротских узлов
            /// </summary>
            private void FindSmallestOrphanNodes(out TreeNode Smallest, out TreeNode Small)
            {
                Smallest = Small = null;
                ulong Tempvalue = ulong.MaxValue - 1;
                TreeNode TempNode = null;
                int i, j = 0;
                int ArrSize = OrphanNodes.Count - 1;
                for (i = ArrSize; i != -1; --i)
                {
                    TempNode = (TreeNode) OrphanNodes[i];
                    if (TempNode.Value >= Tempvalue) continue;
                    Tempvalue = TempNode.Value;
                    Smallest = TempNode;
                    j = i;
                }
                OrphanNodes.RemoveAt(j);
                --ArrSize;

                Tempvalue = ulong.MaxValue - 1;
                for (i = ArrSize; i > -1; --i)
                {
                    TempNode = (TreeNode) OrphanNodes[i];
                    if (TempNode.Value >= Tempvalue) continue;
                    Tempvalue = TempNode.Value;
                    Small = TempNode;
                    j = i;
                }
                OrphanNodes.RemoveAt(j);

            }
        }
    }
}
