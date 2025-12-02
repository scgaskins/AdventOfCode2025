using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day1
{
    internal class Day1Part1 : ISolution
    {

        public long Solve(String filePath)
        {
            int dial = 50;
            int zeroCount = 0;
            foreach (String line in File.ReadLines(filePath))
            {
                int value = int.Parse(line.Substring(1));
                if (line[0] == 'R')
                {
                    dial += value;
                }
                else
                {
                    dial -= value;
                }
                dial = (dial % 100 + 100) % 100;
                if (dial == 0)
                {
                    zeroCount++;
                }
            }
            return zeroCount;
        }
    }
}
