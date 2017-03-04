using System.IO;
using System.Windows.Forms;

namespace HuffmanDecode.Model
{
    public class HuffmanService: IHuffmanService
    {
        /// <summary>Проверка готовности файла к чтению-записи, true - готов </summary>
        /// <param name="filePath">Путь к файлу</param>
        public bool IsSourceAndOutputOK(string filePath)
        {
            bool test = true;
            if (Path.GetFileName(filePath).Length == 0)
            {
                MessageBox.Show("Invalid output file path", "Output file error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                test = false;
            }
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Invalid source file path", "Source file error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                test = false;
            }
            return test;
        }
    }
}
