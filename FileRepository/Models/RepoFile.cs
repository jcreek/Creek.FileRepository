﻿using System;
using System.IO;

namespace FileRepository.Models
{
    /// <summary>
    /// This model serves to represent the files being stored.
    /// </summary>
    public class RepoFile
    {
        /// <summary>
        /// Gets or sets the id field for the file, this serves as the key field/unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the actual file name of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the contents of the file, as a stream for easy manipulation.
        /// </summary>
        public Stream Content { get; set; }

        /// <summary>
        /// Gets or sets the datetime object representing when this file object was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Initialise a File model based on passed parameters.
        /// </summary>
        /// <param name="id">The id of the file.</param>
        /// <param name="fileName">The filename of the file.</param>
        /// <param name="content">The content of the file.</param>
        /// <returns>Returns an initialised File object.</returns>
        public RepoFile GenerateFile(string id, string fileName, Stream content)
        {
            RepoFile newFile = new RepoFile()
            {
                Id = id,
                FileName = fileName,
                Content = content,
                Created = DateTime.Now,
            };

            return newFile;
        }
    }
}