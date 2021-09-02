[![Nuget](https://img.shields.io/nuget/v/Creek.FileRepository)](https://nuget.org/packages/Creek.FileRepository)
[![Nuget](https://img.shields.io/nuget/dt/Creek.FileRepository)](https://nuget.org/packages/Creek.FileRepository)

# Creek.FileRepository

An easy way to hook up various file storage mechanisms, following the repository pattern with a factory.

This is available on NuGet - just click one of the buttons above, or search for `Creek.FileRepository` in Visual Studio.

## How to use a repository

### appsettings.json

To utilise a repository you'll need to set up the relevant section in your `appsettings.json`, for example this section for an SFTP repository:

```json
"SftpRepository": {
    "Host": "my-server.local",
    "Port": 22,
    "Username": "usernamegoeshere",
    "Password": "passwordgoeshere",
    "RemoteDirectoryPath": "/home/username/foldername"
  }
```

You can load this into your standard config in a dotnet core app, or manually load it with something like:

```csharp
private static IConfiguration InitConfiguration()
{
    IConfigurationRoot configBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();
    return configBuilder;
}
```

### Loading the repository

Use the enum of RepositoryTypes to select the repository you want, then use the factory to instantiate it.

```csharp
Factory.RepositoryType repositoryType = Factory.RepositoryType.Sftp;
IFileRepository fileRepository = Factory.GetFileRepository(repositoryType, config);
```

### CRUD operations

#### Create

```csharp
RepoFile repoFile = new RepoFile();

// .. Make changes to a Stream contentStream property here

repoFile = repoFile.GenerateFile(filename, contentStream);

await fileRepository.CreateFileAsync(repoFile);
```

#### Read

```csharp
RepoFile repoFile = await fileRepository.ReadFileAsync(filename);
```

#### Update

```csharp
// .. Make changes to the file's Content property here

await fileRepository.UpdateFileAsync(repoFile);
```

#### Delete

```csharp
await fileRepository.DeleteFileAsync(filename);
```

## Storage Repositories

The following storage mechanisms are included:

- Disk [in progress]
- MongoDB [in progress]
- S3 [in progress]
- SFTP
- SQL [in progress]

The appsettings sections for each one that isn't `in progress` are included below.

### SFTP

```json
"SftpRepository": {
    "Host": "",
    "Port": 22,
    "Username": "",
    "Password": "",
    "RemoteDirectoryPath": "/"
  }
```
