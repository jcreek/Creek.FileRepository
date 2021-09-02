using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Creek.FileRepository.Helpers
{
    /// <summary>
    /// A helper class for string methods.
    /// </summary>
    internal static class StringHelper
    {
        /// <summary>
        /// Checks if a filename contains invalid characters.
        /// </summary>
        /// <param name="filename">The filename to be checked.</param>
        /// <returns>Returns true if the filename contains invalid characters.</returns>
        public static bool IsInvalidFileName(this string filename)
        {
            // Check for Windows characters explicitly to ensure the filename is valid for Linux and Windows
            Regex regex = new Regex(@"[<>:'\/\\|\?\*]");
            return regex.IsMatch(filename);
        }

        /// <summary>
        /// Sanitises a filename containing invalid characters.
        /// </summary>
        /// <param name="originalFileName">The filename to sanitise.</param>
        /// <returns>Returns the sanitised filename.</returns>
        internal static string MakeValidFileName(string originalFileName)
        {
            char[] invalids = System.IO.Path.GetInvalidFileNameChars();
            string validFileName = string.Join("_", originalFileName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');

            // Additionally remove Windows characters explicitly, in case we're running on Linux (e.g. in Docker) but need to access the files in Windows
            string[] charsToRemove = new string[] { "<", ">", ":", "\"", "/", "\\", "|", "?", "*" };

            validFileName = RemoveCharsFromString(charsToRemove, validFileName);

            return validFileName;
        }

        /// <summary>
        /// Removes any characters in an array of strings from a string.
        /// </summary>
        /// <param name="charsToRemove">The string array of characters to remove.</param>
        /// <param name="str">The string to modify.</param>
        /// <returns>Returns the modified string.</returns>
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
