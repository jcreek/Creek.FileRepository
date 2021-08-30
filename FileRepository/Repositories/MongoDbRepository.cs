using FileRepository.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace FileRepository.Repositories
{
    /// <summary>
    /// The repository implementation for MongoDb.
    /// </summary>
    internal class MongoDbRepository : IFileRepository
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MongoDbRepository"/> class with configuration.
        /// </summary>
        /// <param name="config">The configuration to initialise the repository with.</param>
        internal MongoDbRepository(IConfiguration config)
        {
            //this.host = config["MongoDbRepository:host"];
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
