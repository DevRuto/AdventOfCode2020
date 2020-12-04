using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day
{
    public class DayXTemplate : IDay
    {
        private readonly string[] _inputs;
        
        public DayXTemplate()
        {
            _inputs = File.ReadAllLines("./data/dayx.txt").Where(line => !string.IsNullOrEmpty(line)).ToArray();
        }

        public void SolvePart1()
        {
            
        }

        public void SolvePart2()
        {
            
        }

        public static void Run()
        {
            var day = new DayXTemplate();
            day.SolvePart1();
            day.SolvePart2();
        }
    }
}