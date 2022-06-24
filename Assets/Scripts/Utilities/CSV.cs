using UnityEngine;

namespace Utilities
{
    public class CSV
    {
        public static readonly int MAXRow = 13;
        public static readonly int MAXCol = 19;
        public int[,] data;

        public static CSV ReadCSV(string path)
        {
            CSV csv = new CSV();
            int[,] returnData = new int[MAXRow, MAXCol];
            int rowCount = 0;
            var data = Resources.Load<TextAsset>(path);
            var splitData = data.text.Split("\n");
            while (true)
            {
                if (rowCount > MAXRow - 1 || rowCount > splitData.Length - 1)
                {
                    break;
                }

                string readData = splitData[rowCount];
                
                string[] splitReadData = readData.Split(",");
                for (int i = 0; i < MAXCol; i++)
                {
                    if (splitReadData.Length <= i) break;
                    string tmpData = splitReadData[i];
                    if (tmpData.Equals("")) continue;
                    int.TryParse(tmpData, out returnData[rowCount, i]);
                }

                rowCount++;
            }

            csv.data = returnData;
            Resources.UnloadAsset(data);
            return csv;
        }

        public int[] GetRow(int rowCount)
        {
            int[] row = new int[MAXCol];
            for (int i = 0; i < MAXCol; i++)
            {
                row[i] = data[rowCount, i];
            }

            return row;
        }
    }
}