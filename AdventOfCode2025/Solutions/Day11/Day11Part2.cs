using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day11
{
    internal class Day11Part2 : ISolution
    {
        private Dictionary<string, string[]> deviceMap = [];

        public long Solve(string filePath)
        {
            deviceMap = [];
            foreach (string line in File.ReadLines(filePath))
            {
                string[] parts = line.Split(": ");
                deviceMap[parts[0]] = parts[1].Split(" ");
            }
            return CountAllPaths("svr", "out", [], []);
        }

        private long CountAllPaths(string node, string goal, HashSet<string> visited, Dictionary<string, long> cache)
        {
            if (node == goal)
            {
                if (visited.Contains("dac") && visited.Contains("fft"))
                    return 1;
                else
                    return 0;
            }
            string cacheKey = node + visited.Contains("dac").ToString() + visited.Contains("fft").ToString();
            if (cache.ContainsKey(cacheKey))
                return cache[cacheKey];
            visited.Add(node);
            long totalPaths = 0;
            foreach (string output in deviceMap[node])
            {
                if (!visited.Contains(output))
                    totalPaths += CountAllPaths(output, goal, visited, cache);
            }
            visited.Remove(node);
            cache[cacheKey] = totalPaths;
            return totalPaths;
        }
    }
}
