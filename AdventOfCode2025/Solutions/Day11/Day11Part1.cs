using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day11
{
    internal class Day11Part1 : ISolution
    {
        public Dictionary<string, string[]> deviceMap = [];

        public long Solve(string filePath)
        {
            deviceMap = [];
            foreach (string line in File.ReadLines(filePath))
            {
                string[] parts = line.Split(": ");
                deviceMap[parts[0]] = parts[1].Split(" ");
            }
            return CountAllPaths("you", "out", []);
        }

        private long CountAllPaths(string node, string goal, HashSet<string> visited)
        {
            if (node == goal)
                return 1;
            visited.Add(node);
            long totalPaths = 0;
            foreach (string output in deviceMap[node])
            {
                if (!visited.Contains(output))
                    totalPaths += CountAllPaths(output, goal, visited);
            }
            visited.Remove(node);
            return totalPaths;
        }
    }
}
