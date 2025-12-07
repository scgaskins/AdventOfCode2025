using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day7
{
    internal class Day7Part2 : ISolution
    {
        public long Solve(string filePath)
        {
            string firstLine = File.ReadLines(filePath).First();
            long[] numTimelines = new long[firstLine.Length];
            int sourceColumn = firstLine.IndexOf('S');
            numTimelines[sourceColumn] = 1;
            foreach (string line in File.ReadLines(filePath).Skip(1))
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (numTimelines[i] > 0 && line[i] == '^')
                    {
                        if (i - 1 >= 0)
                            numTimelines[i - 1] += numTimelines[i];
                        if (i + 1 < numTimelines.Length)
                            numTimelines[i + 1] += numTimelines[i];
                        numTimelines[i] = 0;
                    }
                }
            }
            return numTimelines.Sum();
        }
    }
}
