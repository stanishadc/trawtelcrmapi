using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace TrawtelCRMAPI.Services
{
    public interface IAmazonService
    {
        //Task<byte[]> DownloadFileAsync(string file);

        //Task<bool> UploadFileAsync(IFormFile file);

        //Task<bool> DeleteFileAsync(string fileName, string versionId = "");
    }
    public class AmazonService : IAmazonService
    {
        string S3URL = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AWSFolders")["Url"];
        string BucketName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AWS")["BucketName"];
        string AirlineLogos = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AWSFolders")["AirlineLogos"];
        private readonly IAmazonS3 _s3Client;
        public AmazonService(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }
        public string getAirportlogo(string airlinename)
        {
            return "https://" + BucketName + S3URL + "/" + AirlineLogos + airlinename + ".png";
        }
        public async Task<object> GetAllFiles(string bucketName, string? FileName)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists)
                return $"Bucket {bucketName} does not exist.";
            var request = new ListObjectsV2Request()
            {
                BucketName = bucketName,
                Prefix = FileName
            };
            var result = await _s3Client.ListObjectsV2Async(request);
            var s3Objects = result.S3Objects.Select(s =>
            {
                var urlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = s.Key,
                    Expires = DateTime.UtcNow.AddMinutes(1)
                };
                return new S3ObjectDto()
                {
                    Name = s.Key.ToString(),
                    PresignedUrl = _s3Client.GetPreSignedURL(urlRequest),
                };
            });
            return new object[] { s3Objects };
        }
        public async Task<string> DeleteFileFromS3(string bucketName,string FileName)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return $"Bucket {bucketName} does not exist";
            await _s3Client.DeleteObjectAsync(bucketName, FileName);
            return "Deleted";
        }
        public async Task<string> UploadFileInS3(IFormFile file, string bucketName, string prefix)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return $"Bucket {bucketName} does not exist.";
            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}",
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await _s3Client.PutObjectAsync(request);
            return ($"File {prefix}/{file.FileName} uploaded to S3 successfully!");
        }
    }
    public class S3ObjectDto
    {
        public string? Name { get; set; }
        public string? PresignedUrl { get; set; }
    }
}