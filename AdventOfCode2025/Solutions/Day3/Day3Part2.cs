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
            foreach (String line in File.ReadLines(filePath))
            {
                Console.WriteLine(line);
                List<int>[] digitIndices = new List<int>[10];
                for (int i = 0; i < digitIndices.Length; i++)
                    digitIndices[i] = new List<int>();

                int[] digits = line.Select((c) => c - '0').ToArray();
                for (int i=0; i<digits.Length; i++)
                {
                    digitIndices[digits[i]].Add(i);
                }

                

                long highestJolt = GetJoltage(digits, digitIndices);
                Console.WriteLine("Highest Jolt: " + highestJolt);
                Console.WriteLine();
                sum += highestJolt;
            }
            return sum;
        }

        private long GetJoltage(int[] digits, List<int>[] digitIndices)
        {
            int[] finalIndices = GetDigitIndicesForBank(digits, digitIndices);
            long final = 0;
            foreach (int i in finalIndices)
            {
                final *= 10;
                final += digits[i];
            }
            return final;
        } 

        private int[] GetDigitIndicesForBank(int[] digits, List<int>[] digitIndices)
        {
            HashSet<int>[] finalDigits = new HashSet<int>[10];
            for (int i = 0; i < finalDigits.Length; i++)
                finalDigits[i] = new HashSet<int>();
            finalDigits[9] = finalDigits[9].Union(digitIndices[9].Take(12)).ToHashSet();
            int totalUsed = finalDigits[9].Count();
            Console.WriteLine("Total used: " + totalUsed);
            int lastIndex = finalDigits[9].LastOrDefault(-1);

            while (totalUsed < 12)
            {
                for (int currentDigit = 8; currentDigit >= 0; currentDigit--)
                {
                    bool addedDigit = false;
                    for (int i = digitIndices[currentDigit].Count - 1; i >= 0; i--)
                    {
                        int digitIndex = digitIndices[currentDigit][i];
                        if (!finalDigits[currentDigit].Contains(digitIndex) && NotHigherThanPrev(digitIndex, currentDigit, finalDigits, digits))
                        {
                            finalDigits[currentDigit].Add(digitIndex);
                            totalUsed++;
                            addedDigit = true;
                            if (totalUsed == 12)
                            {
                                return finalDigits.Aggregate((s1, s2) => s1.Union(s2).ToHashSet()).Order().ToArray();
                            }
                            break;
                        }
                    }
                    if (addedDigit)
                        break;
                }
            }
            return finalDigits.Aggregate((s1, s2) => s1.Union(s2).ToHashSet()).Order().ToArray();
        }

        private bool NotHigherThanPrev(int digitIndex, int currentDigit, HashSet<int>[] finalDigits, int[] allDigits)
        {
            for (int i=currentDigit+1; i<finalDigits.Length; i++)
            {
                foreach (int d in finalDigits[i])
                {
                    if (digitIndex < d && AnyOpenSpacesAfterIndex(d, finalDigits, allDigits))
                        return false;
                }
            }
            return true;
        }

        private bool AnyOpenSpacesAfterIndex(int index, HashSet<int>[] finalDigits, int[] allDigits)
        {
            for (int i=index+1; i<allDigits.Length; i++)
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
