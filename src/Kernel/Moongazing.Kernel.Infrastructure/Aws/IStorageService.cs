using Moongazing.Kernel.Infrastructure.Aws.FileTypes;

namespace Moongazing.Kernel.Infrastructure.Aws;

public interface IStorageService
{

    Task<string> UploadFileAsync(FileType fileType, byte[] fileData, string fileName);
    Task<string> UpdateFileAsync(FileType fileType, string key, byte[] fileData);
    Task<bool> DeleteFileAsync(string key);
    Task<byte[]> DownloadFileAsync(string key);
    Task<List<string>> UploadFilesAsync(FileType fileType, Dictionary<string, byte[]> files);
    Task<List<bool>> DeleteFilesAsync(ICollection<string> keys);
    string ExtractFileKeyFromUrl(string url);
}
