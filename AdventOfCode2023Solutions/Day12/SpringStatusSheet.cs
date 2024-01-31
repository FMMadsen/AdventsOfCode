using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2023Solutions.Day12
{
    internal class SpringStatusSheet
    {
        private DamagedGroup[] _DamagedGroups = [];
        private string _Status;
        private Regex _StatusRegex;
        private Regex _GroupsRegex;
        private List<string> _Arrangements = [];
        private int _ArrangementsCount = 0;
        private int _DamagedFields = 0;


        public DamagedGroup[] DamagedGroups { get { return _DamagedGroups; } }
        public string Status { get { return _Status; } }
        public List<string> Arrangements { get { return _Arrangements; } }
        public int ArrangementsCount { get { return _ArrangementsCount; } }

        public SpringStatusSheet(string status, DamagedGroup[]  damagedGroups) 
        {
            _Status = status;
            _DamagedGroups = damagedGroups;

            SetStatusRegex();
            SetGroupsRegex();

            SetDamagedGroupsMinMax();

            SetDamagedGroupsIndex();

            SetArrangements(0, new int[_DamagedGroups.Length]);

            //_ArrangementsCount = CountArrangements(0, new int[_DamagedGroups.Length]);

        }

        private void SetStatusRegex()
        {
            string damaged = @"\#";
            string operational = @"\.";
            string either = @"[\.\#]";
            StringBuilder pattern = new();

            for(int i = 0; i < _Status.Length; i++)
            {
                char sign = _Status[i];

                switch (sign)
                {
                    case '.':
                        pattern.Append(operational);
                        break;
                    case '#':
                        pattern.Append(damaged);
                        break;
                    case '?':
                        pattern.Append(either);
                        break;
                    default:
                        pattern.Append(@"");
                        break;
                }
            }

            _StatusRegex = new Regex(pattern.ToString());
        }

        private void SetGroupsRegex()
        {
            string damaged = @"\#";
            string operational = @"\.*";
            StringBuilder pattern = new();

            pattern.Append(operational);
            for (int i = 0; i < _DamagedGroups.Length; i++)
            {
                if (0<i)
                {
                    pattern.Append(@"\.+");
                }

                pattern.Append(damaged);
                pattern.Append('{');
                pattern.Append(_DamagedGroups[i].Length);
                pattern.Append('}');
            }
            pattern.Append(operational);

            _GroupsRegex = new Regex(pattern.ToString());
        }

        private void SetDamagedGroupsMinMax()
        {
            for (int damagedGroupsIndex = 0; damagedGroupsIndex < _DamagedGroups.Length; damagedGroupsIndex++)
            {
                _DamagedGroups[damagedGroupsIndex].MinIndex = damagedGroupsIndex;
                _DamagedGroups[damagedGroupsIndex].MaxIndex = _Status.Length + 1 - _DamagedGroups.Length + damagedGroupsIndex;

                for (int i = 0; i < damagedGroupsIndex; i++)
                {
                    _DamagedGroups[damagedGroupsIndex].MinIndex += _DamagedGroups[i].Length;
                }

                for (int i = _DamagedGroups.Length-1; damagedGroupsIndex <= i; i--)
                {
                    _DamagedGroups[damagedGroupsIndex].MaxIndex -= _DamagedGroups[i].Length;
                }

            }
            
        }

        private void SetDamagedGroupsIndex()
        {
            Regex test;

            foreach (DamagedGroup group in _DamagedGroups)
            {
                StringBuilder pattern = new();

                pattern.Append(@"[\#\?]");
                pattern.Append('{');
                pattern.Append(group.Length);
                pattern.Append('}');

                test = new Regex(pattern.ToString());

                for (int i = group.MinIndex; i <= group.MaxIndex; i++)
                {
                    Match result = test.Match(Status, i);
                    if (result.Success && !group.Indexes.Contains(result.Index))
                    {
                        group.Indexes.Add(result.Index);
                    }

                }
            }
        }

        private void SetArrangements(int damagedGroupsIndex, int[] damagedGroupsIndexes)
        {
            if (damagedGroupsIndex < damagedGroupsIndexes.Length - 1)
            {
                for (int i = 0; i < _DamagedGroups[damagedGroupsIndex].Indexes.Count; i++)
                {
                    damagedGroupsIndexes[damagedGroupsIndex] = i;
                    SetArrangements(damagedGroupsIndex + 1, damagedGroupsIndexes);
                }
            }
            else
            {
                for (int i = 0; i < _DamagedGroups[damagedGroupsIndex].Indexes.Count; i++)
                {
                    damagedGroupsIndexes[damagedGroupsIndex] = i;

                    BuildArrangement(damagedGroupsIndexes);

                }
            }
        }

        private void BuildArrangement(int[] damagedGroupsIndexes)
        {
            //if (!TestCase(damagedGroupsIndexes)) { return; }

            StringBuilder sb = new();
            int damagedGroupsIndex = -1;
            int gStart = -1;
            int gEnd = -1;

            for (int statusIndex = 0; statusIndex < _Status.Length; statusIndex++)
            {
                if (gEnd < statusIndex && damagedGroupsIndex+1 < damagedGroupsIndexes.Length)
                {
                    damagedGroupsIndex++;
                    gStart = _DamagedGroups[damagedGroupsIndex].Indexes[damagedGroupsIndexes[damagedGroupsIndex]];
                    gEnd = gStart + _DamagedGroups[damagedGroupsIndex].Length - 1;
                }

                if (gStart <= statusIndex && statusIndex <= gEnd)
                {
                    sb.Append('#');
                }
                else
                {
                    sb.Append('.');
                }

            }

            string test = sb.ToString();

            if (_StatusRegex.Match(test).Success && _GroupsRegex.Match(test).Success)
            {
                _Arrangements.Add(test);
            }

            
        }

        private int CountArrangements(int damagedIndex, int[] damagedIndexes)
        {
            int sum = 0;

            if (damagedIndex < damagedIndexes.Length - 1)
            {
                for (int i = 0; i < _DamagedGroups[damagedIndex].Indexes.Count; i++)
                {
                    damagedIndexes[damagedIndex] = i;
                    sum += CountArrangements(damagedIndex+1, damagedIndexes);
                }
            }
            else
            {
                for (int i = 0; i < _DamagedGroups[damagedIndex].Indexes.Count; i++)
                {
                    damagedIndexes[damagedIndex] = i;

                    sum += TestCase(damagedIndexes) ? 1 : 0;

                }
            }

            return sum;
        }

        private bool TestCase(int[] damagedGroupsIndexes)
        {
            int damagedGroupsIndex = 0;

            for (int statusIndex = 0; statusIndex < _Status.Length; statusIndex++)
            {
                int index = _DamagedGroups[damagedGroupsIndex].Indexes[damagedGroupsIndexes[damagedGroupsIndex]];

                if (index < statusIndex) { return false; }

                while (statusIndex < index)
                {
                    if ('#' == _Status[statusIndex]) { return false; }
                    statusIndex++;
                }

                statusIndex = index + _DamagedGroups[damagedGroupsIndex].Length;

                if (statusIndex < _Status.Length)
                {
                    if ('#' == _Status[statusIndex]) { return false; }
                }
                else if (damagedGroupsIndex < damagedGroupsIndexes.Length -1 ) { return false; }
                else { break; }

                damagedGroupsIndex++;

                if (damagedGroupsIndex >= damagedGroupsIndexes.Length)
                {
                    while (statusIndex < _Status.Length)
                    {
                        if ('#' == _Status[statusIndex]) { return false; }
                        statusIndex++;
                    }
                }
            }

            return true;
        }
    }
}
