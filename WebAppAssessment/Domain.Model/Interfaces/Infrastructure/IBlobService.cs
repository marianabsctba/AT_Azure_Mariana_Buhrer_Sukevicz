using System.IO;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Infrastructure
{
    public interface IBlobService
    {
        Task<string> UploadAsync(Stream stream);
        Task DeleteAsync(string BlobName);
        Task<string> UpdateAsync(string blobName, Stream stream);

    }
}
