using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LambdaForums.Service.ImageService
{
    public class UploadService : IUploadService
    {
        public CloudBlobContainer GetBlobContainer(string connectionString, string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerName);
        }

        public async Task<string> UploadImageToStorage(IFormFile file, string connectionString, string containerName)
        {
            // Get Blob Container
            var container = GetBlobContainer(connectionString, containerName);

            // Parse the Content Disposition response header
            var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            // Grab the file name
            var filename = Path.Combine(parsedContentDisposition.FileName.Trim('"'));

            // Get a reference to a Block Blob
            var blockBlob = container.GetBlockBlobReference(filename);

            // On that block Blob, Upload our file <-- file uploaded to the cloud 
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            return blockBlob.Uri.AbsoluteUri;
        }
    }
}
