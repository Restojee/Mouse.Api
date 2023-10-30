using Minio;

namespace Mouse.NET.Common;

public interface IMinioService
{
    MinioClient MinioClient { get; }
}