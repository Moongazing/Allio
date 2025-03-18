using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Options;
using Moongazing.Kernel.Infrastructure.Aws.Configuration;
using Moongazing.Kernel.Infrastructure.Aws.FileTypes;

namespace Moongazing.Kernel.Infrastructure.Aws;

public class StorageService : IStorageService
{
    private readonly AwsS3Settings awsS3Settings;
    private readonly IAmazonS3 s3Client;
    private readonly TransferUtility transferUtility;

    public StorageService(IOptions<AwsS3Settings> awsS3Settings,
                          IAmazonS3 s3Client)
    {
        this.awsS3Settings = awsS3Settings.Value;
        this.s3Client = s3Client;
        transferUtility = new TransferUtility(s3Client);
    }

    public async Task<string> UploadFileAsync(FileType fileType, byte[] fileData, string fileName)
    {
        var key = GenerateFileKey(fileType, fileName);

        using var stream = new MemoryStream(fileData);
        var request = new TransferUtilityUploadRequest
        {
            InputStream = stream,
            Key = key,
            BucketName = awsS3Settings.BucketName,
            CannedACL = S3CannedACL.PublicRead
        };

        await transferUtility.UploadAsync(request).ConfigureAwait(false);
        return GenerateFileUrl(key);
    }

    public async Task<string> UpdateFileAsync(FileType fileType, string key, byte[] fileData)
    {
        await DeleteFileAsync(key).ConfigureAwait(false);
        return await UploadFileAsync(fileType, fileData, ExtractFileNameFromKey(key)).ConfigureAwait(false);
    }

    public async Task<bool> DeleteFileAsync(string key)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = awsS3Settings.BucketName,
            Key = key
        };

        var response = await s3Client.DeleteObjectAsync(deleteObjectRequest).ConfigureAwait(false);
        return response.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
    }

    public async Task<byte[]> DownloadFileAsync(string key)
    {
        var getObjectRequest = new GetObjectRequest
        {
            BucketName = awsS3Settings.BucketName,
            Key = key
        };

        using var response = await s3Client.GetObjectAsync(getObjectRequest).ConfigureAwait(false);
        using var memoryStream = new MemoryStream();
        await response.ResponseStream.CopyToAsync(memoryStream).ConfigureAwait(false);

        return memoryStream.ToArray();
    }

    public async Task<List<string>> UploadFilesAsync(FileType fileType, Dictionary<string, byte[]> files)
    {
        var urls = new List<string>();

        foreach (var kvp in files)
        {
            var url = await UploadFileAsync(fileType, kvp.Value, kvp.Key);
            urls.Add(url);
        }

        return urls;
    }

    public async Task<List<bool>> DeleteFilesAsync(ICollection<string> keys)
    {
        var results = new List<bool>();

        foreach (var key in keys)
        {
            var result = await DeleteFileAsync(key);
            results.Add(result);
        }

        return results;
    }

    private static string GenerateFileKey(FileType fileType, string fileName)
    {
        return $"{fileType.ToString().ToLower()}/{Guid.NewGuid()}_{fileName}";
    }

    private string GenerateFileUrl(string key)
    {
        return $"{awsS3Settings.ServiceURL}/{awsS3Settings.BucketName}/{key}";
    }

    public string ExtractFileKeyFromUrl(string url)
    {
        var uri = new Uri(url);
        return uri.AbsolutePath[(uri.AbsolutePath.LastIndexOf('/') + 1)..];
    }

    private static string ExtractFileNameFromKey(string key)
    {
        return key[(key.LastIndexOf('/') + 1)..];
    }
}
