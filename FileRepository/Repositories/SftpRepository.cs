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
    internal class SftpRepository : IFileRepository
    {
        private string host;
        private int port;
        private string username;
        private string password;
        private string remoteDirectoryPath;

        internal SftpRepository(IConfiguration config)
        {
            this.host = config["SftpRepository:host"];
            this.port = int.Parse(config["SftpRepository:port"]);
            this.username = config["SftpRepository:username"].ToString();
            this.password = config["SftpRepository:password"].ToString();
            this.remoteDirectoryPath = config["SftpRepository:remoteDirectoryPath"].ToString();
        }

        public async Task<string> CreateFileAsync(RepoFile file)
        {
            bool canOverride = false;
            UploadFile(file, canOverride);

            return file.FileName;
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            using (var client = new SftpClient(this.host, this.port == 0 ? 22 : this.port, this.username, this.password))
            {
                try
                {
                    client.Connect();
                    client.DeleteFile($"{this.remoteDirectoryPath}/{fileName}");
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

        public async Task<RepoFile> ReadFileAsync(string fileName)
        {
            RepoFile repoFile = new RepoFile();

            using (SftpClient client = new SftpClient(this.host, this.port == 0 ? 22 : this.port, this.username, this.password))
            {
                try
                {
                    client.Connect();

                    client.DownloadFile($"{this.remoteDirectoryPath}/{fileName}", output: repoFile.Content);

                    repoFile = repoFile.GenerateFile(fileName, repoFile.Content);

                    //_logger.LogInformation($"Finished downloading file [{localFilePath}] from [{remoteFilePath}]");
                }
                catch (Exception exception)
                {
                    //_logger.LogError(exception, $"Failed in downloading file [{localFilePath}] from [{remoteFilePath}]");
                }
                finally
                {
                    client.Disconnect();
                }
            }

            return repoFile;
        }

        public async Task<bool> UpdateFileAsync(RepoFile file)
        {
            bool canOverride = true;
            UploadFile(file, canOverride);

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

                    client.UploadFile(file.Content, file.FileName, canOverride);

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
