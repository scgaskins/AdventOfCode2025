using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day8
{
    internal class Day8Part1 : ISolution
    {
        public long Solve(string filePath)
        {
            List<Pair<Coordinate>> pairs = ReadInCoordinates(filePath);
            int numConnections = filePath.Contains("test.txt") ? 10 : 1000;
            List<HashSet<Coordinate>> circuits = ConnectCircuits(pairs, numConnections);
            circuits.Sort((a, b) => a.Count.CompareTo(b.Count));
            return circuits.TakeLast(3).Select((c) => c.Count).Aggregate((a, b) => a * b);
        }

        List<HashSet<Coordinate>> ConnectCircuits(List<Pair<Coordinate>> pairs, int numConnections)
        {
            Dictionary<int, HashSet<Coordinate>> circuits = [];
            Dictionary<Coordinate, int> coordToIndex = [];
            int idCounter = 0;
            foreach (Pair<Coordinate> pair in pairs.Take(numConnections))
            {
                int firstIndex = coordToIndex.GetValueOrDefault(pair.A, -1);
                int secondIndex = coordToIndex.GetValueOrDefault(pair.B, -1);
                int finalIndex;
                if (firstIndex == -1 && secondIndex == -1)
                {
                    circuits.Add(idCounter, [pair.A, pair.B]);
                    finalIndex = idCounter;
                    idCounter++;
                }
                else if (firstIndex == -1)
                {
                    circuits[secondIndex].Add(pair.A);
                    finalIndex = secondIndex;
                }
                else if (secondIndex == -1)
                {
                    circuits[firstIndex].Add(pair.B);
                    finalIndex = firstIndex;
                }
                else if (firstIndex != secondIndex)
                {
                    HashSet<Coordinate> firstCircuit = circuits[firstIndex];
                    circuits[secondIndex].UnionWith(firstCircuit);
                    circuits.Remove(firstIndex);
                    finalIndex = secondIndex;
                    foreach (Coordinate coord in firstCircuit)
                        coordToIndex[coord] = finalIndex;
                }
                else
                {
                    finalIndex = firstIndex;
                }
                coordToIndex[pair.A] = finalIndex;
                coordToIndex[pair.B] = finalIndex;
            }
            return circuits.Values.ToList();
        }

        private List<Pair<Coordinate>> ReadInCoordinates(string filePath)
        {
            Coordinate[] coords = ReadFile(filePath);
            return PairUpCoordinates(coords);
        }

        private Coordinate[] ReadFile(string filePath)
        {
            return File.ReadLines(filePath).Select((line) => {
                int[] parts = line.Split(",").Select(int.Parse).ToArray();
                return new Coordinate(parts[0], parts[1], parts[2]);
            }).ToArray();
        }

        private List<Pair<Coordinate>> PairUpCoordinates(Coordinate[] coords)
        {
            List<Pair<Coordinate>> pairs = [];
            for (int i=0; i<coords.Length; i++)
            {
                for (int j=i+1; j<coords.Length; j++)
                {
                    pairs.Add(new Pair<Coordinate>(coords[i], coords[j]));
                }
            }
            pairs.Sort((pair1, pair2) => pair1.A.GetDistance(pair1.B).CompareTo(pair2.A.GetDistance(pair2.B)));
            return pairs;
        }

        private class Pair<E>(E a, E b)
        {
            public E A { get; private set; } = a;
            public E B { get; private set; } = b;

            public override bool Equals(object? obj)
            {
                if (obj is not Pair<E> other)
                    return false;
                return other.A.Equals(A) && other.B.Equals(B);
            }

            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }

            public override string ToString()
            {
                return '(' + A.ToString() + ',' + B.ToString() + ')';
            }
        }

        private class Coordinate(int x, int y, int z)
        {
            public int X { get; private set; } = x;
            public int Y { get; private set; } = y;
            public int Z { get; private set; } = z;

            public double GetDistance(Coordinate other)
            {
                return Math.Sqrt(
                    Math.Pow(X - other.X, 2) + 
                    Math.Pow(Y - other.Y, 2) + 
                    Math.Pow(Z - other.Z, 2)
                    );
            }

            public override bool Equals(object? obj)
            {
                if (obj is not Coordinate other)
                    return false;
                return other.X == X && other.Y == Y && other.Z == Z;
            }

            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }

            public override string ToString()
            {
                return '(' + X.ToString() + ',' + Y.ToString() + ',' + Z.ToString() + ')';
            }
        }
    }
}
