using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day1
{
    internal class Day1Part2 : ISolution
    {
        private String filePath;

        public Day1Part2(String filePath)
        {
            this.filePath = filePath;
        }

        public long Solve()
        {
            int dial = 50;
            int zeroCount = 0;
            foreach (String line in File.ReadLines(filePath))
            {
                int value = int.Parse(line.Substring(1));
                if (line[0] == 'R')
                {
                    dial += value;
                    zeroCount += dial / 100;
                }
                else
                {
                    int start = dial;
                    dial -= value;
                    if (dial <= 0)
                    {
                        if (start != 0)
                            zeroCount += 1;
                        zeroCount += (-dial) / 100;
                    }
                }
                dial = (dial % 100 + 100) % 100;
            }
            return zeroCount;
        }
    }
}
