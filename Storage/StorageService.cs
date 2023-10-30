using Minio;
using Mouse.NET.Common;

namespace Mouse.NET.Storage;

public class StorageService : IStorageService
{
    private readonly IMinioService minioService;

    public StorageService(IMinioService minioService)
    {
        this.minioService = minioService;
    }

    public async Task<string> Upload(IFormFile  file)
    {
        string fileName = Guid.NewGuid().ToString();

        try
        {
            await this.minioService.MinioClient.PutObjectAsync(
                new PutObjectArgs()
                    .WithBucket("maps")
                    .WithObject(fileName)
                    .WithStreamData(file.OpenReadStream())
                    .WithObjectSize(file.Length)
                    .WithContentType(file.ContentType)
            );

            return fileName;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("File upload failed.", e);
        }
    }
}