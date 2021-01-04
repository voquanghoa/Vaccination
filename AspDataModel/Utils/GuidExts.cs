using System;

namespace AspDataModel.Utils
{
    public static class GuidExts
    {
        public static string FormatAsString(this Guid guid)
        {
            return guid.ToString("N");
        }

        public static Guid FromString(this string str)
        {
            return Guid.Parse(str);
        }
    }
}