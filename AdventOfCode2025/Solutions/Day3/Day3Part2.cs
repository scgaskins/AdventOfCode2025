using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day3
{
    internal class Day3Part2 : ISolution
    {

        public long Solve(string filePath)
        {
            long sum = 0;
            foreach (string line in File.ReadLines(filePath))
            {
                Console.WriteLine(line);
                JoltageBank joltageBank = new JoltageBank(line);
                long highestJolt = joltageBank.GetJoltage();
                Console.WriteLine("Highest Jolt: " + highestJolt);
                Console.WriteLine();
                sum += highestJolt;
            }
            return sum;
        }

        private class JoltageBank
        {
            private int[] digits;
            private List<int>[] digitIndices;
            HashSet<int>[] finalDigits;

            public JoltageBank(string line)
            {
                digits = line.Select((c) => c - '0').ToArray();

                digitIndices = new List<int>[10];
                for (int i = 0; i < digitIndices.Length; i++)
                    digitIndices[i] = new List<int>();
                for (int i = 0; i < digits.Length; i++)
                    digitIndices[digits[i]].Add(i);

                finalDigits = new HashSet<int>[10];
                for (int i = 0; i < finalDigits.Length; i++)
                    finalDigits[i] = new HashSet<int>();
                finalDigits[9] = finalDigits[9].Union(digitIndices[9].Take(12)).ToHashSet();
            }

            public long GetJoltage()
            {
                while (finalDigits.Sum((s) => s.Count) < 12)
                {
                    AddNextDigit();
                }
                int[] finalIndices = finalDigits.Aggregate((s1, s2) => s1.Union(s2).ToHashSet()).Order().ToArray();
                long final = 0;
                foreach (int i in finalIndices)
                {
                    final *= 10;
                    final += digits[i];
                }
                return final;
            }

            private void AddNextDigit()
            {
                for (int currentDigit = 8; currentDigit >= 0; currentDigit--)
                {
                    for (int i = digitIndices[currentDigit].Count - 1; i >= 0; i--)
                    {
                        int digitIndex = digitIndices[currentDigit][i];
                        if (!finalDigits[currentDigit].Contains(digitIndex) && NotHigherThanPrev(digitIndex))
                        {
                            finalDigits[currentDigit].Add(digitIndex);
                            return;
                        }
                    }
                }
            }

            // Checks that the digit at the index is not a higher
            // place value than a higher value digit unless there are
            // no open spaces after that digit
            private bool NotHigherThanPrev(int digitIndex)
            {
                int currentDigit = digits[digitIndex];
                for (int i = currentDigit + 1; i < finalDigits.Length; i++)
                {
                    foreach (int d in finalDigits[i])
                    {
                        if (digitIndex < d && AnyOpenSpacesAfterIndex(d))
                            return false;
                    }
                }
                return true;
            }

            private bool AnyOpenSpacesAfterIndex(int index)
            {
                for (int i = index + 1; i < digits.Length; i++)
                {
                    bool spaceIsOpen = true;
                    foreach (HashSet<int> digits in finalDigits)
                    {
                        if (digits.Contains(i))
                        {
                            spaceIsOpen = false;
                            break;
                        }
                    }
                    if (spaceIsOpen)
                        return true;
                }
                return false;
            }
        }
    }
}
