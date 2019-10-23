using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace LambdaForums.Service.ImageService
{
    public interface IUploadService
    {
        CloudBlobContainer GetBlobContainer(string connectionString, string containerName);

        Task<string> UploadImageToStorage(IFormFile file, string connectionString, string containerName);
    }
}
