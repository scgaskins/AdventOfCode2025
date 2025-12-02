using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Solutions.Day2
{
    internal class Day2Part1 : ISolution
    {

        public long Solve(String filePath)
        {
            long sum = 0;
            String[] ranges = File.ReadAllText(filePath).Split(",");
            foreach (String range in ranges)
            {
                String[] ids = range.Split("-");
                long startValue = ClosestInvalidId(ids[0]);
                long lowerNum = long.Parse(ids[0]);
                long higherNum = long.Parse(ids[1]);
                if (startValue < lowerNum)
                    startValue = NextInvalidId(startValue);
                for (long value = startValue; value <= higherNum; value = NextInvalidId(value))
                {
                    sum += value;
                }
            }
            return sum;
        }

        private long ClosestInvalidId(String id)
        {
            String nextId;
            if (id.Length % 2 == 0)
                nextId = id.Substring(0, id.Length / 2);
            else
                nextId = 1 + (new String('0', id.Length / 2));
            nextId += nextId;
            return long.Parse(nextId);
        }

        private long NextInvalidId(long id)
        {
            int powerOfTen = (int) Math.Floor(Math.Log10(id));
            if (id + 1 == ((long)Math.Pow(10, powerOfTen + 1)))
                return ClosestInvalidId((id + 1).ToString());
            long adder = ((long)Math.Pow(10, (powerOfTen / 2) + 1)) + 1;
            return id + adder;
        }
    }
}
