namespace ToolsFramework
{
    public static class StringValidator
    {
        public static bool IsUnevenLength(string txt)
            => txt.Length % 2 > 0;

        public static bool AreStringsEqual(params string[] strings)
        {
            if (strings.Length == 0)
                return true;
            return strings.All(s => s.Equals(strings[0]));
        }

        public static bool AreAllCharsEqual(string str)
        {
            if (str.Length == 0)
                return true;
            return str.All(s => s.Equals(str[0]));
        }

        /// <summary>
        /// Following are true
        /// 1212
        /// 111
        /// 123123
        /// 1
        /// and empty string
        /// 
        /// False is
        /// 12
        /// 123
        /// 1234
        /// 1110
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CanSplitIntoEqualParts(string str)
        {
            if (str.Length < 2)
                return false;

            if (str.Length == 3)
                return AreAllCharsEqual(str);
            
            if (AreAllCharsEqual(str))
                return true;

            //At this point there are dealt with all the cases of length 3 and below
            //We have to deal with all cases of length that are 4+
            // 121212 or 145145 123412341234

            var length = str.Length;
            var maxSequenceSize = str.Length / 2;

            for (int s = 2; s <= maxSequenceSize; s++)
            {
                var strings = StringTools.SplitIntoPartsOfSize(str, s);
                if (AreStringsEqual(strings))
                    return true;
            }

            return false;
        }
    }
}
