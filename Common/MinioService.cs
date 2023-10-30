using Minio;
using Mouse.NET.Common;

public class MinioService : IMinioService
{
    public MinioClient MinioClient { get; }

    public MinioService(IConfiguration configuration)
    {
        MinioClient = new MinioClient()
            .WithCredentials(configuration["Minio:AccessKey"], configuration["Minio:SecretKey"])
            .WithEndpoint(configuration["Minio:Endpoint"])
             .Build();
    }
}