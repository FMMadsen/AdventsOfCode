using AdventOfCode2024Solutions.Day16;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024Solutions.Day10
{
    public class CharConverter<T>
    {
        private T AnEnum;
        private Dictionary<T, Func<GameObject>> EnumToTypeList;
        private Dictionary<char, T> CharToEnumList;
    }
}
