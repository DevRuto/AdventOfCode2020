using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day
{
    public class DayX : IDay
    {
        private readonly string[] _inputs;
        
        public DayX()
        {
            _inputs = File.ReadAllLines("./data/dayx.txt").Where(line => !string.IsNullOrEmpty(line)).ToArray();
        }

        public void SolvePart1()
        {

            Console.WriteLine("Day X\t> Part 1\n\t\t");
        }

        public void SolvePart2()
        {
            
            Console.WriteLine("\t> Part 2");
        }

        public static void Run()
        {
            var day = new DayX();
            day.SolvePart1();
            day.SolvePart2();
        }
    }
}