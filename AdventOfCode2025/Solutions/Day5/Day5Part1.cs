using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day5
{
    internal class Day5Part1 : ISolution
    {
        public long Solve(string filePath)
        {
            List<Range> ranges = [];
            bool readingRanges = true;
            int freshCount = 0;
            foreach (string line in File.ReadLines(filePath))
            {
                if (readingRanges)
                {
                    if (!String.IsNullOrEmpty(line))
                        ranges.Add(new Range(line));
                    else
                        readingRanges = false;
                } else
                {
                    if (ranges.Any((r) => r.InRange(long.Parse(line))))
                        freshCount++;
                }
                    
            }
            return freshCount;
        }

        private class Range
        {
            public long Bottom { get; private set; }
            public long Top { get; private set; }

            public Range(string rangeString)
            {
                long[] elements = rangeString.Split("-").Select(long.Parse).ToArray();
                Bottom = elements[0];
                Top = elements[1];
            }

            public bool InRange(long i)
            {
                return Bottom <= i && i <= Top;
            }
        }
    }
}
