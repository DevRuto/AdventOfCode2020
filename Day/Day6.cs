using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day
{
    public class Day6 : IDay
    {
        private readonly string[] _groups;
        
        public Day6()
        {
            _groups = File.ReadAllText("./data/day6.txt").Split(new[] { "\r\n\r\n", "\n\n", "\r\r" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
        }

        public void SolvePart1()
        {
            var counts = new List<int>();
            foreach (var group in _groups)
            {
                var trim = Regex.Replace(group, @"\r\n?|\n", "");
                var count = trim.Distinct().Count();
                counts.Add(count);
            }
            Console.WriteLine("Day 6\t> Part 1\n\t\t Sum of counts: {0}", counts.Sum());
        }

        public void SolvePart2()
        {
            var counts = new List<int>();
            foreach (var group in _groups)
            {
                var people = group.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                char[] diff = people[0].ToCharArray();
                foreach (var person in people)
                {
                    diff = person.Intersect(diff).ToArray();
                }
                counts.Add(diff.Length);
            }
            Console.WriteLine("\t> Part 2\n\t\t Sum of counts: {0}", counts.Sum());
        }

        public static void Run()
        {
            var day = new Day6();
            day.SolvePart1();
            day.SolvePart2();
        }
    }
}