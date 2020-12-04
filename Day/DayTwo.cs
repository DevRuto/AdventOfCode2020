using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day
{
    public class DayTwo : IDay
    {
        private readonly string[] _inputs;

        public DayTwo()
        {
            _inputs= File.ReadAllLines("./data/day2.txt").Where(line => !string.IsNullOrEmpty(line)).ToArray();
        }

        public void SolvePart1()
        {
            int valid = 0;
            foreach (var line in _inputs) // ex. "1-3 a: abcde"
            {
                var spl = line.Split(' ');

                var part = spl[0].Split('-');
                int min = int.Parse(part[0]), max = int.Parse(part[1]);
                
                char letter = spl[1].Substring(0, 1)[0];

                var password = spl[2];
                int letterCount = password.Where(c => c == letter).Count();
                if (letterCount >= min && letterCount <= max)
                    valid++;
            }
            Console.WriteLine("Day Two Part 1: {0} valid passwords", valid);
        }

        public void SolvePart2()
        {
            int valid = 0;
            foreach (var line in _inputs) // ex. "1-3 a: abcde"
            {
                var spl = line.Split(' ');

                var part = spl[0].Split('-');
                int pos1 = int.Parse(part[0]) - 1, pos2 = int.Parse(part[1]) - 1;
                
                char letter = spl[1].Substring(0, 1)[0];

                var password = spl[2];
                int letterCount = password.Where(c => c == letter).Count();
                if (password[pos1] == letter ^ password[pos2] == letter)
                    valid++;
            }
            Console.WriteLine("Day Two Part 2: {0} valid passwords", valid);
        }

        public static void Run()
        {
            var day = new DayTwo();
            day.SolvePart1();
            day.SolvePart2();
        }
    }
}