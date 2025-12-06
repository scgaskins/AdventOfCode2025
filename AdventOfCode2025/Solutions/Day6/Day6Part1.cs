using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day6
{
    internal class Day6Part1 : ISolution
    {
        private List<List<long>> problems;

        public Day6Part1()
        {
            problems = [];
        }

        public long Solve(string filePath)
        {
            long grandTotal = 0;
            foreach (string line in File.ReadLines(filePath))
            {
                if (line.StartsWith('+') || line.StartsWith('*'))
                {
                    string[] parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (parts[i] == "+")
                            grandTotal += problems[i].Sum();
                        else if (parts[i] == "*")
                            grandTotal += problems[i].Aggregate((j, k) => j * k);
                    }
                }
                else
                {
                    long[] nums = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                    for (int i = 0; i < nums.Length; i++)
                    {
                        if (problems.Count < (i + 1))
                            problems.Add([]);
                        problems[i].Add(nums[i]);
                    }
                }
            }
            return grandTotal;
        }
    }
}
