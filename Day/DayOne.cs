using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day
{
    public class DayOne
    {
        private readonly List<int> _inputs = new List<int>();

        public DayOne()
        {
            _inputs.AddRange(File.ReadAllLines("./data/day1.txt").Select(line => int.Parse(line)));
            _inputs.Sort();
            _inputs.Reverse();
        }

        public void SolvePart1()
        {
            // Don't want to touch original list
            var list = new List<int>(_inputs);
            int sumGoal = 2020;
            int num1 = 0;
            int num2 = 0;
            do
            {
                // Higher number
                num1 = list.First();
                // Lower number
                num2 = list.Last();
                
                if (num1 + num2 == sumGoal) break;
                if (num1 + num2 < sumGoal)
                {
                    // Remove this low number, it will be too low for any other number
                    list.Remove(num2);
                }
                else
                {
                    // Remove high number, it will be too high for any other number
                    list.Remove(num1);
                }
            } while (list.Count >= 2);
            Console.WriteLine("Day 1 Part 1\n\t{0} * {1} = {2}", num1, num2, num1 * num2);
        }

        public void SolvePart2()
        {
            // Don't want to touch original list
            var list = new List<int>(_inputs);
            int sumGoal = 2020;
            int num1 = 0, num2 = 0, num3 = 0, curSum = 0;
            do
            {
                // Higher number
                num1 = list.First();
                num2 = list[list.Count - 2];
                // Lower number
                num3 = list.Last();
                curSum = num1 + num2 + num3;
                
                if (curSum == sumGoal) break;
                if (curSum > sumGoal)
                {
                    list.Remove(num1);
                    continue;
                }

                // Don't think I like this next part but brain couldn't work at the moment
                for (int i = 1; i < list.Count - 1; i++)
                {
                    num2 = list[i];
                    curSum = num1 + num2 + num3;
                    if (curSum == sumGoal) 
                    {
                        list.Clear();
                        break;
                    }
                }
                list.Remove(num1);
            } while (list.Count >= 3);
            Console.WriteLine("Day 1 Part 2\n\t{0} * {1} * {2} = {3}", num1, num2, num3, num1 * num2 * num3);
        }

        public static void Run()
        {
            var day = new DayOne();
            day.SolvePart1();
            day.SolvePart2();
        }
    }
}