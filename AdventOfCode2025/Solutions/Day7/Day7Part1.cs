using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day7
{
    internal class Day7Part1 : ISolution
    {
        public long Solve(string filePath)
        {
            string firstLine = File.ReadLines(filePath).First();
            bool[] tachyonColumns = new bool[firstLine.Length];
            int sourceColumn = firstLine.IndexOf('S');
            tachyonColumns[sourceColumn] = true;
            long splitCount = 0;
            foreach (string line in File.ReadLines(filePath).Skip(1))
            {
                for (int i=0; i<line.Length; i++)
                {
                    if (tachyonColumns[i] && line[i] == '^')
                    {
                        splitCount++;
                        tachyonColumns[i] = false;
                        if (i - 1 >= 0)
                            tachyonColumns[i - 1] = true;
                        if (i + 1 < tachyonColumns.Length)
                            tachyonColumns[i + 1] = true;
                    }
                }
            }
            return splitCount;
        }
    }
}
