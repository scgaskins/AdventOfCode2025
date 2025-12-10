using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day10
{
    internal class Day10Part1 : ISolution
    {
        public long Solve(string filePath)
        {
            long sum = 0;
            foreach (string line in File.ReadLines(filePath))
            {
                string[] parts = line.Split(" ");
                int bitPattern = PatternToBits(parts[0]);
                int[] buttons = parts.Where((p) => p.StartsWith('('))
                    .Select(ButtonToBits).ToArray();
                int fewestPresses = FewestPressesToGoal(0, bitPattern, buttons, [], []);
                sum += fewestPresses;
                Console.WriteLine(fewestPresses);
            }
            return sum;
        }

        private int FewestPressesToGoal(int lightPattern, int goal, int[] buttons, HashSet<int> visitedStates, Dictionary<int, int> cache)
        {
            if (lightPattern == goal)
                return 0;
            if (cache.ContainsKey(lightPattern))
                return cache[lightPattern];
            int fewestPresses = int.MaxValue;
            visitedStates.Add(lightPattern);
            foreach (int button in buttons)
            {
                int newPattern = lightPattern ^ button;
                if (!visitedStates.Contains(newPattern))
                {
                    int pressesFromNew = FewestPressesToGoal(newPattern, goal, buttons, visitedStates, cache);
                    int presses = pressesFromNew == int.MaxValue ? int.MaxValue : pressesFromNew + 1;
                    fewestPresses = Math.Min(presses, fewestPresses);
                }
            }
            visitedStates.Remove(lightPattern);
            cache[lightPattern] = fewestPresses;
            return fewestPresses;
        }

        private int PatternToBits(string pattern)
        {
            int bitPattern = 0;
            for (int i=1; i<pattern.Length; i++)
            {
                if (pattern[i] == '#')
                {
                    int bit = 1 << (i - 1);
                    bitPattern ^= bit;
                }
            }
            return bitPattern;
        }

        private int ButtonToBits(string buttonPattern)
        {
            int[] indices = buttonPattern.Substring(1, buttonPattern.Length - 2).Split(",").Select(int.Parse).ToArray();
            int bitPattern = 0;
            foreach (int i in indices)
            {
                int bit = 1 << i;
                bitPattern ^= bit;
            }
            return bitPattern;
        }
    }
}
