using FileRepository.Models;
using Microsoft.Extensions.Configuration;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileRepository.Repositories
{
    /// <summary>
    /// The repository implementation for SFTP.
    /// </summary>
    internal class SftpRepository : IFileRepository
    {
        private readonly string host;
        private readonly int port;
        private readonly string username;
        private readonly string password;
        private readonly string remoteDirectoryPath;

        /// <summary>
        /// Initialises a new instance of the <see cref="SftpRepository"/> class with configuration.
        /// </summary>
        /// <param name="config">The configuration to initialise the repository with.</param>
        internal SftpRepository(IConfiguration config)
        {
            this.host = config["SftpRepository:host"];
            this.port = int.Parse(config["SftpRepository:port"]);
            this.username = config["SftpRepository:username"].ToString();
            this.password = config["SftpRepository:password"].ToString();
            this.remoteDirectoryPath = config["SftpRepository:remoteDirectoryPath"].ToString();
        }

        /// <summary>
        /// Create operation for the repository.
        /// </summary>
        /// <param name="file">The file to be created.</param>
        /// <returns>Returns the filename.</returns>
        public async Task<string> CreateFileAsync(RepoFile file)
        {
            bool canOverride = false;
            this.UploadFile(file, canOverride);

            return file.Filename;
        }

        /// <summary>
        /// Delete operation for the repository.
        /// </summary>
        /// <param name="filename">The filename to be deleted.</param>
        /// <returns>Returns true if deletion was successful.</returns>
        public async Task<bool> DeleteFileAsync(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException($"'{nameof(filename)}' cannot be null or empty.", nameof(filename));
            }

            using (var client = new SftpClient(this.host, this.port == 0 ? 22 : this.port, this.username, this.password))
            {
                try
                {
                    client.Connect();
                    client.DeleteFile($"{this.remoteDirectoryPath}/{filename}");
                    // _logger.LogInformation($"File [{remoteFilePath}] deleted.");
                }
                catch (Exception exception)
                {
                    // _logger.LogError(exception, $"Failed in deleting file [{remoteFilePath}]");
                    throw;
                }
                finally
                {
                    client.Disconnect();
                }
            }

            return true;
        }

        /// <summary>
        /// Read operation for the repository.
        /// </summary>
        /// <param name="filename">The filename to be read.</param>
        /// <returns>Returns the RepoFile if reading was successful.</returns>
        public async Task<RepoFile> ReadFileAsync(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException($"'{nameof(filename)}' cannot be null or empty.", nameof(filename));
            }

            RepoFile repoFile = new RepoFile();
            repoFile.Content = new System.IO.MemoryStream();

            using (SftpClient client = new SftpClient(this.host, this.port == 0 ? 22 : this.port, this.username, this.password))
            {
                try
                {
                    client.Connect();

                    client.DownloadFile($"{this.remoteDirectoryPath}/{filename}", output: repoFile.Content);

                    repoFile = repoFile.GenerateFile(filename, repoFile.Content);

                    //_logger.LogInformation($"Finished downloading file [{localFilePath}] from [{remoteFilePath}]");
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex, $"Failed in downloading file [{localFilePath}] from [{remoteFilePath}]");
                    throw;
                }
                finally
                {
                    client.Disconnect();
                }
            }

            return repoFile;
        }

        /// <summary>
        /// Update operation for the repository.
        /// </summary>
        /// <param name="file">The file to be updated.</param>
        /// <returns>Returns true if update was successful.</returns>
        public async Task<bool> UpdateFileAsync(RepoFile file)
        {
            bool canOverride = true;
            this.UploadFile(file, canOverride);

            return true;
        }

        private void UploadFile(RepoFile file, bool canOverride)
        {
            using (SftpClient client = new SftpClient(this.host, this.port == 0 ? 22 : this.port, this.username, this.password))
            {
                try
                {
                    client.Connect();
                    client.ChangeDirectory(this.remoteDirectoryPath);

                    client.UploadFile(file.Content, file.Filename, canOverride);

                    //_logger.LogInformation($"Finished uploading file {file.FileName} to [{remoteDirectory}]");
                }
                // TODO - handle specific exceptions where a file already exists and cannot be overwritten
                catch (Exception ex)
                {
                    //_logger.LogError(ex, $"Failed in uploading file {file.FileName} to [{remoteDirectory}]");

                    throw;
                }
                finally
                {
                    client.Disconnect();
                }
            }
        }
    }
}
