using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day10
{
    internal class Day10Part2 : ISolution
    {
        public long Solve(string filePath)
        {
            long sum = 0;
            foreach (string line in File.ReadLines(filePath))
            {
                string[] parts = line.Split(" ");
                int[][] buttons = parts.Where((p) => p.StartsWith('('))
                    .Select(PatternToArray).ToArray();
                int[] goalCounts = PatternToArray(parts.Last());
                JoltageIndicator goal = new JoltageIndicator(goalCounts);
                int fewestPresses = FewestPressesToGoal(new JoltageIndicator(goalCounts.Length), goal, buttons, []);
                sum += fewestPresses;
                Console.WriteLine(fewestPresses);
            }
            return sum;
        }

        private int FewestPressesToGoal(JoltageIndicator indicator, JoltageIndicator goal, int[][] buttons, Dictionary<JoltageIndicator, int> cache)
        {
            if (indicator.Equals(goal))
                return 0;
            if (indicator.OverOther(goal))
                return int.MaxValue;
            if (cache.ContainsKey(indicator))
                return cache[indicator];
            int fewestPresses = int.MaxValue;
            foreach (int[] button in buttons)
            {
                JoltageIndicator newPattern = indicator.UpdateCountsAtIndices(button);
                if (!newPattern.OverOther(goal))
                {
                    int pressesFromNew = FewestPressesToGoal(newPattern, goal, buttons, cache);
                    int presses = pressesFromNew == int.MaxValue ? int.MaxValue : pressesFromNew + 1;
                    fewestPresses = Math.Min(presses, fewestPresses);
                }
            }
            cache[indicator] = fewestPresses;
            return fewestPresses;
        }

        private int[] PatternToArray(string pattern)
        {
            return pattern.Trim(['(', ')', '{', '}'])
                .Split(',')
                .Select(int.Parse)
                .ToArray();
        }

        private class JoltageIndicator
        {
            private int[] counts;

            public JoltageIndicator(int[] counts)
            {
                this.counts = counts;
            }

            public JoltageIndicator(int size)
            {
                counts = new int[size];
            }

            public override string ToString()
            {
                return String.Join(',', counts);
            }

            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }

            public override bool Equals(object? obj)
            {
                if (obj is not JoltageIndicator other)
                    return false;
                return counts.SequenceEqual(other.counts);
            }

            public JoltageIndicator UpdateCountsAtIndices(int[] indices)
            {
                int[] newCounts = new int[counts.Length];
                Array.Copy(counts, newCounts, newCounts.Length);
                foreach (int index in indices)
                {
                    newCounts[index]++;
                }
                return new JoltageIndicator(newCounts);
            }

            public bool OverOther(JoltageIndicator other)
            {
                for (int i=0; i<counts.Length; i++)
                {
                    if (counts[i] > other.counts[i])
                        return true;
                }
                return false;
            }
        }
    }
}
