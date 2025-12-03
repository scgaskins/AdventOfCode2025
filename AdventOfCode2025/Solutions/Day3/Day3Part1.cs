using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day3
{
    internal class Day3Part1 : ISolution
    {
        public long Solve(string filePath)
        {
            long sum = 0;
            foreach (String line in File.ReadLines(filePath))
            {
                int highestJolt = int.MinValue;
                int[] digits = line.Select((c) => c - '0').ToArray();
                for (int i=0; i<digits.Length; i++)
                {
                    int tensDigit = 10 * digits[i];
                    for (int j=i+1; j<digits.Length; j++)
                    {
                        int num = tensDigit + digits[j];
                        highestJolt = Math.Max(highestJolt, num);
                    }
                }
                sum += highestJolt;
            }
            return sum;
        }
    }
}
