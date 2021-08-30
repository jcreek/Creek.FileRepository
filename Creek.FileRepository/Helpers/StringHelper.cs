using System;
using System.Collections.Generic;
using System.Text;

namespace Creek.FileRepository.Helpers
{
    internal class StringHelper
    {
        public static bool IsInvalidFileName(string filename)
        {
            char[] invalidCharacters = new char[] { '<', '>', ':', '\'', '/', '\\', '|', '?', '*' };

            return Array.Exists(invalidCharacters, character => filename.Contains(character.ToString()));
        }

        internal static string MakeValidFileName(string originalFileName)
        {
            char[] invalids = System.IO.Path.GetInvalidFileNameChars();
            string validFileName = String.Join("_", originalFileName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');

            // Additionally remove Windows characters explicitly, in case we're running on Linux (e.g. in Docker) but need to access the files in Windows
            string[] charsToRemove = new string[] { "<", ">", ":", "\"", "/", "\\", "|", "?", "*" };

            validFileName = RemoveCharsFromString(charsToRemove, validFileName);

            return validFileName;
        }

        internal static string RemoveCharsFromString(string[] charsToRemove, string str)
        {
            foreach (string c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }

            return str;
        }
    }
}
