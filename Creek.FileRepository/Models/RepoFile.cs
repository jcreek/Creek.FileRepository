using System;
using System.IO;

namespace Creek.FileRepository.Models
{
    /// <summary>
    /// This model serves to represent the files being stored.
    /// </summary>
    public class RepoFile
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="RepoFile"/> class.
        /// </summary>
        /// <param name="filename">The filename of the file.</param>
        /// <param name="created">The DateTime the file was created.</param>
        /// <param name="content">The content of the file.</param>
        public RepoFile(string filename, DateTime created, Stream content = null)
        {
            this.Filename = filename;
            this.Content = content ?? new MemoryStream();
            this.Created = created;
        }

        /// <summary>
        /// Gets or sets the actual file name of the file, this serves as the key field/unique identifier.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the contents of the file, as a stream for easy manipulation.
        /// </summary>
        public Stream Content { get; set; }

        /// <summary>
        /// Gets or sets the datetime object representing when this file object was created.
        /// </summary>
        public DateTime Created { get; set; }
    }
}
