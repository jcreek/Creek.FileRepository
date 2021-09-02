using Creek.FileRepository;
using Creek.FileRepository.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using UnitTests.Helpers;

namespace UnitTests
{
    public class SftpRepositoryShould
    {
        private readonly IConfiguration config = InitConfiguration();
        private readonly Factory.RepositoryType repositoryType = Factory.RepositoryType.Sftp;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldNotCreateAFileWithAnInvalidFilename()
        {
            Assert.That(() => CreateTestFile("create><invalid?/test.txt"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public async Task ShouldCreateAFileWithoutThrowing()
        {
            try
            {
                await CreateTestFile("createtest.txt");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public async Task ShouldDeleteAFile()
        {
            try
            {
                await DeleteTestFile("createtest.txt");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public async Task ShouldReadAFile()
        {
            string filename = "readtest.txt";

            // Setup
            try
            {
                // Create a file to read
                await CreateTestFile(filename);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to create a file to read - {ex.Message}");
            }

            try
            {
                // Read the file
                IFileRepository fileRepository = Factory.GetFileRepository(repositoryType, config);

                RepoFile repoFile = await fileRepository.ReadFileAsync(filename);

                Assert.NotNull(repoFile);
                Assert.NotNull(repoFile.Filename);
                Assert.NotNull(repoFile.Content);
                Assert.NotNull(repoFile.Created);
            }
            catch (Exception ex)
            {
                // Clean up
                await DeleteTestFile(filename);

                Assert.Fail(ex.Message);
            }

            // Clean up
            await DeleteTestFile(filename);
        }

        [Test]
        public async Task ShouldUpdateAFile()
        {
            string filename = "updatetest.txt";

            // Setup file to read
            try
            {
                // Create a file to read
                await CreateTestFile(filename);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed to create a file to read - {ex.Message}");
            }

            // Read the file and update it
            try
            {
                IFileRepository fileRepository = Factory.GetFileRepository(repositoryType, config);
                RepoFile repoFile = await fileRepository.ReadFileAsync(filename);

                // Modify the content and update the file
                string updateTestText = "update test text";
                repoFile.Content = StreamHelper.GenerateStreamFromString(updateTestText);
                await fileRepository.UpdateFileAsync(repoFile);

                // Load the file anew to check for changes
                RepoFile repoFileUpdated = await fileRepository.ReadFileAsync(filename);

                // Move the pointer back to the beginning of the stream so we can read it here
                repoFileUpdated.Content.Seek(0, SeekOrigin.Begin);

                using (StreamReader reader = new StreamReader(repoFileUpdated.Content))
                {
                    string contents = await reader.ReadToEndAsync();
                    Assert.AreEqual(updateTestText, contents);
                }
            }
            catch (Exception ex)
            {
                // Clean up
                await DeleteTestFile(filename);

                Assert.Fail(ex.Message);
            }

            // Clean up
            await DeleteTestFile(filename);
        }

        private async Task CreateTestFile(String filename)
        {
            IFileRepository fileRepository = Factory.GetFileRepository(repositoryType, config);

            Stream contentStream = StreamHelper.GenerateStreamFromString("a,b \n c,d");

            RepoFile repoFile = new RepoFile(filename, DateTime.Now, contentStream);

            await fileRepository.CreateFileAsync(repoFile);
        }

        private async Task DeleteTestFile(String filename)
        {
            IFileRepository fileRepository = Factory.GetFileRepository(repositoryType, config);

            await fileRepository.DeleteFileAsync(filename);
        }

        private static IConfiguration InitConfiguration()
        {
            IConfigurationRoot configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return configBuilder;
        }
    }
}
