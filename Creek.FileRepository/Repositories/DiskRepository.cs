using Creek.FileRepository.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Creek.FileRepository.Repositories
{
    /// <summary>
    /// The repository implementation for Disk.
    /// </summary>
    internal class DiskRepository : IFileRepository
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DiskRepository"/> class with configuration.
        /// </summary>
        /// <param name="config">The configuration to initialise the repository with.</param>
        internal DiskRepository(IConfiguration config)
        {
            //this.host = config["DiskRepository:host"];
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
