namespace Mouse.NET.Storage;

public interface IStorageService
{
    public Task<string> Upload(IFormFile file);
}