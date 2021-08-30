using FileRepository.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace FileRepository.Repositories
{
    /// <summary>
    /// The repository implementation for SQL.
    /// </summary>
    internal class SqlRepository : IFileRepository
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SqlRepository"/> class with configuration.
        /// </summary>
        /// <param name="config">The configuration to initialise the repository with.</param>
        internal SqlRepository(IConfiguration config)
        {
            //this.host = config["SqlRepository:host"];
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
