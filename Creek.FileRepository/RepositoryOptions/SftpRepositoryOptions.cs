using System;
using System.Collections.Generic;
using System.Text;

namespace Creek.FileRepository.RepositoryOptions
{
    public class SftpRepositoryOptions
    {
        public const string SftpRepository = "SftpRepository";

        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RemoteDirectoryPath { get; set; }
    }
}
