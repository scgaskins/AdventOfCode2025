using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day9
{
    internal class Day9Part1 : ISolution
    {
        public long Solve(string filePath)
        {
            Coordinate[] coords = File.ReadLines(filePath)
                .Select((line) => new Coordinate(line))
                .ToArray();
            long largestRectangle = long.MinValue;
            for (int i=0; i<coords.Length; i++)
            {
                for (int j=i+1; j<coords.Length; j++)
                {
                    largestRectangle = Math.Max(largestRectangle, coords[i].RectangleArea(coords[j]));
                }
            }
            return largestRectangle;
        }

        private class Coordinate
        {
            public long X { get; private set; }
            public long Y { get; private set; }

            public Coordinate(string coordString)
            {
                long[] parts = coordString.Split(",").Select(long.Parse).ToArray();
                X = parts[0];
                Y = parts[1];
            }

            public Coordinate(long x, long y)
            {
                X = x;
                Y = y;
            }

            public long RectangleArea(Coordinate other)
            {
                long length = Math.Abs(X - other.X) + 1;
                long height = Math.Abs(Y - other.Y) + 1;
                return length * height;
            }
        }
    }
}
