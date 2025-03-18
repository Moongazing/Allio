namespace Moongazing.Kernel.Infrastructure.Aws.Configuration;

public class AwsS3Settings
{
    public string Profile { get; set; } = default!;
    public string Region { get; set; } = default!;
    public string BucketName { get; set; } = default!;
    public string AccessKey { get; set; } = default!;
    public string SecretKey { get; set; } = default!;
    public string ServiceURL { get; set; } = default!;
    public string[] AcceptedFileFormats { get; set; } = default!;
}
