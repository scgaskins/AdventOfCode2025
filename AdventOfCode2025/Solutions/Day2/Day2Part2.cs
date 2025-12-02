using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day2
{
    internal class Day2Part2 : ISolution
    {
        private String filePath;

        public Day2Part2(String filePath)
        {
            this.filePath = filePath;
        }

        public long Solve()
        {
            long sum = 0;
            Range[] ranges = File.ReadAllText(filePath).Split(",").Select((p) => new Range(p)).ToArray();
            HashSet<long> checkedValues = new HashSet<long>();
            for (int i=1; i<100000; i++)
            {
                long value = AddOnPattern(i, i);
                while (value < 10000000000L)
                {
                    if (!checkedValues.Contains(value))
                    {
                        checkedValues.Add(value);
                        foreach (Range range in ranges)
                        {
                            if (range.InsideRange(value))
                            {
                                Console.WriteLine(value);
                                sum += value;
                                break;
                            }
                        }
                    }
                    value = AddOnPattern(value, i);
                }
            }
            return sum;
        }

        private long AddOnPattern(long head, int pattern)
        {
            int powerOfTen = (int)Math.Floor(Math.Log10(pattern));
            return (head * ((long)Math.Pow(10, powerOfTen + 1))) + pattern;
        }

        private class Range
        {
            public long Bottom { get; private set; }
            public long Top { get; private set; }

            public Range(String rangeString)
            {
                String[] parts = rangeString.Split("-");
                Bottom = long.Parse(parts[0]);
                Top = long.Parse(parts[1]);
            }

            public bool InsideRange(long x)
            {
                return Bottom <= x && x <= Top;
            }
        }
    }
}
