using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day5
{
    internal class Day5Part2 : ISolution
    {
        public long Solve(string filePath)
        {
            List<Range> ranges = [];
            foreach (string line in File.ReadLines(filePath))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    Range r = new Range(line);
                    for (int i=ranges.Count-1; i>=0; i--)
                    {
                        if (r.Overlap(ranges[i]))
                        {
                            r = r.Combine(ranges[i]);
                            ranges.RemoveAt(i);
                        }
                    }
                    ranges.Add(r);
                }
                else
                    break;
            }
            return ranges.Select((r) => r.Size).Sum();
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

            public Range(long bottom, long top)
            {
                Bottom = bottom;
                Top = top;
            }

            public long Size
            {
                get
                {
                    return (Top - Bottom) + 1;
                }
            }

            public bool InRange(long i)
            {
                return Bottom <= i && i <= Top;
            }

            public bool Overlap(Range other)
            {
                return InRange(other.Top) || InRange(other.Bottom)
                    || other.InRange(Top) || other.InRange(Bottom);
            }

            public Range Combine(Range other)
            {
                long bottom = Math.Min(Bottom, other.Bottom);
                long top = Math.Max(Top, other.Top);
                return new Range(bottom, top);
            }
        }
    }
}
