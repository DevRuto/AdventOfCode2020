using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day
{
    public class Day7 : IDay
    {
        private readonly string[] _inputs;
        
        public Day7()
        {
            _inputs = File.ReadAllLines("./data/day7.txt").Where(line => !string.IsNullOrEmpty(line)).ToArray();
        }

        public void SolvePart1()
        {
            var bagColl = new BagCollection();
            foreach (var line in _inputs)
            {
                bagColl.Parse(line);
            }

            int count = 0;
            var targetBag = bagColl.GetBag("shiny", "gold");
            foreach (Bag bag in bagColl)
                if (bag.CanHold(targetBag))
                    count++;

            Console.WriteLine("Day 7\t> Part 1\n\t\t Count: {0}", count);
        }

        public void SolvePart2()
        {
            var bagColl = new BagCollection();
            foreach (var line in _inputs)
            {
                bagColl.Parse(line);
            }

            int count = 0;
            var targetBag = bagColl.GetBag("shiny", "gold");
            Console.WriteLine("\t> Part 2\n\t\t Total bags: {0}", targetBag.TotalBags - 1);
        }

        public static void Run()
        {
            var day = new Day7();
            // day.SolvePart1();
            day.SolvePart2();
        }

        private class BagCollection : ICollection<Bag>
        {
            private readonly List<Bag> _bagCache = new List<Bag>();

            public int Count => _bagCache.Count;

            public bool IsSynchronized => false;

            public object SyncRoot => this;

            public bool IsReadOnly => throw new NotImplementedException();

            public void CopyTo(Bag[] array, int index)
            {
                foreach (var bag in _bagCache)
                {
                    array.SetValue(bag, index++);
                }
            }

            public void Parse(string line)
            {
                var bag = new Bag();
                var split = line.TrimEnd('.').Split(' ');
                bag.Type = split[0];
                bag.Color = split[1];
                if (!_bagCache.Contains(bag))
                    _bagCache.Add(bag);
                else
                    bag = _bagCache.First(b => b.Type == bag.Type && b.Color == bag.Color);
                
                if (split[4] == "no")
                    return;

                for (int i = 4; i < split.Length; i += 4)
                {
                    var type = split[i+1];
                    var color = split[i+2];
                    var quantity = int.Parse(split[i]);
                    var b = new Bag { Type = type, Color = color };
                    if (_bagCache.Contains(b))
                        b = _bagCache.First(bc => bc.Type == b.Type && bc.Color == b.Color);
                    else
                        _bagCache.Add(b);
                    if (!bag.Content.ContainsKey(b))
                        bag.Content.Add(b, quantity);
                }
            }

            public void Add(Bag item)
            {
                _bagCache.Add(item);
            }

            public Bag GetBag(string type, string color)
            {
                return _bagCache.First(b => b.Type == type && b.Color == color);
            }

            public void Clear()
            {
                _bagCache.Clear();
            }

            public bool Contains(Bag item)
            {
                return _bagCache.Contains(item);
            }

            public bool Remove(Bag item)
            {
                return _bagCache.Remove(item);
            }

            IEnumerator<Bag> IEnumerable<Bag>.GetEnumerator()
            {
                return (IEnumerator<Bag>)GetEnumerator();
            }

            public IEnumerator GetEnumerator()
            {
                return new Enumerator(_bagCache);
            }

            public class Enumerator : IEnumerator<Bag>
            {
                private Bag[] _bags;
                private int _cursor;

                Bag IEnumerator<Bag>.Current => _bags[_cursor];

                public object Current => _bags[_cursor];

                public Enumerator(IEnumerable<Bag> bags)
                {
                    _bags = bags.ToArray();
                    _cursor = -1;
                }

                public bool MoveNext()
                {
                     if (_cursor < _bags.Length)
                        _cursor++;

                    return(!(_cursor == _bags.Length));
                }

                public void Reset()
                {
                    _cursor = -1;
                }

                public void Dispose()
                {
                    
                }
            }
        }

        private class Bag : IEquatable<Bag>
        {
            public string Type { get; set; }
            public string Color { get; set; }
            public readonly Dictionary<Bag, int> Content = new Dictionary<Bag, int>();

            public bool CanHold(Bag bag)
            {
                var canHold = false;
                foreach (var (b, quantity) in Content)
                {
                    if (canHold) break;
                    if (b == bag)
                        canHold = true;
                    else
                        canHold = b.CanHold(bag);
                }
                return canHold;
            }

            public int TotalBags
            {
                get
                {
                    int count = 1;
                    foreach (var (b, quantity) in Content)
                    {
                        count += quantity * b.TotalBags;
                    }
                    return count;
                }
            }

            public bool Equals(Bag otherBag)
            {
                return Type == otherBag.Type && Color == otherBag.Color;
            }
        }
    }
}