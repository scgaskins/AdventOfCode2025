using AdventOfCode2025.Solutions;
using AdventOfCode2025.Solutions.Day1;
using AdventOfCode2025.Solutions.Day10;
using AdventOfCode2025.Solutions.Day2;
using AdventOfCode2025.Solutions.Day3;
using AdventOfCode2025.Solutions.Day4;
using AdventOfCode2025.Solutions.Day5;
using AdventOfCode2025.Solutions.Day6;
using AdventOfCode2025.Solutions.Day7;
using AdventOfCode2025.Solutions.Day8;
using AdventOfCode2025.Solutions.Day9;

namespace AdventOfCode2025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                String inputFileDirectory = args[0];
                int daySelection = ReadIntFromUser("What day do you want to solve?");
                int partSelection = ReadIntFromUser("What part do you want to solve?");
                bool isTesting = ReadBoolFromUser("Do you want to use test input?");
                String filePath = inputFileDirectory + "\\Day"
                    + daySelection + "\\" + (isTesting ? "test.txt" : "input");
                ISolution solution = GetSolution(daySelection, partSelection, filePath);
                if (solution == null)
                    Console.WriteLine("Could not find solution.");
                else
                {
                    Console.WriteLine(solution.Solve(filePath));
                }
                if (ReadBoolFromUser("Do you want to quit the program?"))
                    break;
            }
        }

        private static bool ReadBoolFromUser(String prompt)
        {
            bool bInput;
            while (true)
            {
                Console.WriteLine(prompt + " (y/n)");
                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();
                if (key.KeyChar == 'y' || key.KeyChar == 'Y')
                {
                    bInput = true;
                    break;
                }
                else if (key.KeyChar == 'n' || key.KeyChar == 'N')
                {
                    bInput = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter y or n");
                }
            }
            return bInput;
        }

        private static int ReadIntFromUser(String prompt)
        {
            int intInput;
            while (true)
            {
                Console.WriteLine(prompt);
                String? input = Console.ReadLine();
                if (!String.IsNullOrEmpty(input) && int.TryParse(input, out intInput))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("That is not a valid input.");
                }
            }
            return intInput;
        }

        private static ISolution GetSolution(int day, int part, String filePath)
        {
            if (day == 1)
            {
                if (part == 1)
                    return new Day1Part1();
                else if (part == 2)
                    return new Day1Part2();
            } else if (day == 2)
            {
                if (part == 1)
                    return new Day2Part1();
                else if (part == 2)
                    return new Day2Part2();
            } else if (day == 3)
            {
                if (part == 1)
                    return new Day3Part1();
                else if (part == 2)
                    return new Day3Part2();
            } else if (day == 4)
            {
                if (part == 1)
                    return new Day4Part1();
                else if (part == 2)
                    return new Day4Part2();
            } else if (day == 5)
            {
                if (part == 1)
                    return new Day5Part1();
                else if (part == 2)
                    return new Day5Part2();
            } else if (day == 6)
            {
                if (part == 1)
                    return new Day6Part1();
                else if (part == 2)
                    return new Day6Part2();
            } else if (day == 7)
            {
                if (part == 1)
                    return new Day7Part1();
                else if (part == 2)
                    return new Day7Part2();
            } else if (day == 8)
            {
                if (part == 1)
                    return new Day8Part1();
                else if (part == 2)
                    return new Day8Part2();
            } else if (day == 9)
            {
                if (part == 1)
                    return new Day9Part1();
                else if (part == 2)
                    return new Day9Part2();
            } else if (day == 10)
            {
                if (part == 1)
                    return new Day10Part1();
                else if (part == 2)
                    return new Day10Part2();
            }
                return null;
        }
    }
}
