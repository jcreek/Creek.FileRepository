using Creek.FileRepository.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Creek.FileRepository.Repositories
{
    /// <summary>
    /// The repository implementation for S3.
    /// </summary>
    internal class S3Repository : IFileRepository
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="S3Repository"/> class with configuration.
        /// </summary>
        /// <param name="config">The configuration to initialise the repository with.</param>
        internal S3Repository(IConfiguration config)
        {
            //this.host = config["S3Repository:host"];
        }

        public async Task<string> CreateFileAsync(RepoFile file)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteFileAsync(string filename)
        {
            throw new NotImplementedException();
        }

        public async Task<RepoFile> ReadFileAsync(string filename)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateFileAsync(RepoFile file)
        {
            throw new NotImplementedException();
        }
    }
}
