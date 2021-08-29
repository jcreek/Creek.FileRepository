using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileRepository.Models
{
    public class File
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public Stream Content { get; set; }
        public DateTime Created { get; set; }

        public File GenerateFile(string id, string fileName, Stream content)
        {
            File newFile = new File()
            {
                Id = id,
                FileName = fileName,
                Content = content,
                Created = DateTime.Now
            };

            return newFile;
        }
    }
}
