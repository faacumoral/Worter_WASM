using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FMCW.Common
{
    public static class Helpers
    {
        public static Dictionary<string, string> ReplaceableValues = new Dictionary<string, string>
        {
            { "ß", "ss"},
            { "ä", "a"}, { "á", "a"},
            { "ë", "e"}, { "é", "e"},
            { "ï", "i"}, { "í", "i"},
            { "ö", "o"}, { "ó", "o"},
            { "ü", "u"}, { "ú", "u"}
        };

        public static T ReadJson<T>(string path)
        {
            if (!File.Exists(path))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path)); ;
        }

        public static string GetSearchValue(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;

            str = str.ToLower();
            foreach (var replace in ReplaceableValues)
            {
                str = str.Replace(replace.Key, replace.Value);
            }
            return str;
        }
    }
}
