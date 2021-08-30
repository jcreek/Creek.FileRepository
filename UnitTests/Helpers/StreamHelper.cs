﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnitTests.Helpers
{
    public class StreamHelper
    {
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
