using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day4
{
    internal class Day4Part1 : ISolution
    {
        bool[][] papers;

        public Day4Part1()
        {
            papers = new bool[1][];
        }

        public long Solve(string filePath)
        {
            papers = File.ReadLines(filePath).Select((l) => l.Select((c) => c == '@').ToArray()).ToArray();
            long count = 0;
            for (int r=0; r<papers.Length; r++)
            {
                for (int c=0; c < papers[r].Length; c++)
                {
                    Position p = new Position(r, c);
                    if (PosOccupied(p) && ForkliftAccessible(p))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private bool ForkliftAccessible(Position pos)
        {
            int occupiedNeighbors = 0;
            foreach (Position n in pos.GetAdjacentPositions())
            {
                if (PosInGrid(n) && PosOccupied(n))
                {
                    occupiedNeighbors++;
                    if (occupiedNeighbors >= 4)
                        return false;
                }
            }
            return occupiedNeighbors < 4;
        }

        private bool PosOccupied(Position pos)
        {
            return papers[pos.Row][pos.Col];
        }

        private bool PosInGrid(Position position)
        {
            return position.Row >= 0 && position.Row < papers.Length
                && position.Col >= 0 && position.Col < papers[position.Row].Length;
        }

        private class Position
        {
            public int Row { get; private set; }
            public int Col { get; private set; }

            public Position(int r, int c)
            {
                Row = r;
                Col = c;
            }

            public Position[] GetAdjacentPositions()
            {
                return [
                    new Position(Row-1, Col), new Position(Row-1, Col+1),
                    new Position(Row, Col+1), new Position(Row+1, Col+1),
                    new Position(Row+1,Col), new Position(Row+1, Col-1),
                    new Position(Row, Col-1), new Position(Row-1, Col-1)
                    ];
            }
        }
    }
}
