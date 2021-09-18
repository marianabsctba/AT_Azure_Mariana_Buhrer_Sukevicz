using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Domain.Model.Interfaces.Infrastructure;

namespace Infra.Services.Blobs
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private const string _containerName = "containerfinal";

        public BlobService(string connectionString)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadAsync(Stream stream)
        {
            var container = _blobServiceClient.GetBlobContainerClient(_containerName);

            await container.CreateIfNotExistsAsync();
            await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            var blobClient = container.GetBlobClient($"{Guid.NewGuid()}.jpg");

            await blobClient.UploadAsync(stream);
            return blobClient.Uri.ToString();
        }

        public async Task<string> UpdateAsync(string blobName, Stream stream)
        {
            await DeleteAsync(blobName);

            var container = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = container.GetBlobClient($"{Guid.NewGuid()}.jpg");

            await blobClient.UploadAsync(stream);

            return blobClient.Uri.ToString();
        }

        public async Task DeleteAsync(string blobName)
        {
            var container = _blobServiceClient.GetBlobContainerClient(_containerName);

            var blobClient = container.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
