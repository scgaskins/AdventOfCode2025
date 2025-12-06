using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day6
{
    internal class Day6Part2 : ISolution
    {
        private List<List<long>> problems;

        public Day6Part2()
        {
            problems = [];
        }

        public long Solve(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            ReadInProblems(lines);
            foreach (List<long> problem in problems)
            {
                Console.WriteLine("Problem: ");
                Console.WriteLine("  " + String.Join(", ", problem));
            }
            long grandTotal = 0;
            string[] parts = lines.Last().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "+")
                    grandTotal += problems[i].Sum();
                else if (parts[i] == "*")
                    grandTotal += problems[i].Aggregate((j, k) => j * k);
            }
            return grandTotal;
        }

        private void ReadInProblems(string[] lines)
        {
            int numberOfInputLines = lines.Length - 1;
            List<long> currentInput = [];
            for (int i=0; i < lines[0].Length; i++)
            {
                long value = 0;
                for (int j=0; j<numberOfInputLines; j++)
                {
                    int digit = Char.IsWhiteSpace(lines[j][i]) ? 0 : lines[j][i] - '0';
                    if (value > 0 && digit == 0)
                        break;
                    value *= 10;
                    value += digit;
                }
                if (value > 0)
                {
                    currentInput.Add(value);
                } else
                {
                    problems.Add(currentInput);
                    currentInput = [];
                }
            }
            problems.Add(currentInput);
        }
    }
}
