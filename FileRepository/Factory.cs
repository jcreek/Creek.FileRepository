using System;
using System.Collections.Generic;
using System.Text;

namespace FileRepository
{
    /// <summary>
    /// A factory to enable consumers of this package to easily get a specific type of repository.
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// An enum to restrict users to only select valid repository types.
        /// </summary>
        public enum RepositoryType
        {
            /// <summary>
            /// An enum member for storing files on disk.
            /// </summary>
            Disk,

            /// <summary>
            /// An enum member for storing files in a MongoDb NoSQL database.
            /// </summary>
            MongoDb,

            /// <summary>
            /// An enum member for storing files on AWS S3.
            /// </summary>
            S3,

            /// <summary>
            /// An enum member for storing files on a remote drive using SFTP.
            /// </summary>
            Sftp,

            /// <summary>
            /// An enum member for storing files in an SQL database.
            /// </summary>
            Sql,
        }

        /// <summary>
        /// Initialise an implementation of IFileRepository based on a selected enum member.
        /// </summary>
        /// <param name="repositoryType">The type of repository to initialise, based on the enum member.</param>
        /// <returns>Returns an initialised repository.</returns>
        public IFileRepository GetFileRepository(RepositoryType repositoryType)
        {
            throw new NotImplementedException("This section won't work until another method is set up with at least one repository type.");

            //IFileRepository fileRepository = // Here we need to get the specific class of repository based on the RepositoryType enum.

            //return fileRepository;
        }
    }
}
