using System.IO;

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
            StreamReader streamReader = new StreamReader(path);

            while (true)
            {
                string readData = streamReader.ReadLine();

                if (readData == null || rowCount > MAXRow - 1)
                {
                    break;
                }

                string[] splitReadData = readData.Split(",");

                for (int i = 0; i < MAXCol; i++)
                {
                    string tmpData = splitReadData[i];
                    if (tmpData.Equals("")) continue;
                    returnData[rowCount, i] = int.Parse(tmpData);
                }

                rowCount++;
            }

            csv.data = returnData;
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