using System.Threading.Tasks;

namespace EventuallyAPI.Services
{
    public interface IFileStorage
    {
        public Task<string> SaveFile(byte[] content, string fileExtension, string container, string contentType, string FileName);
        public Task RemoveFile(string route, string container);
        public Task<string> EditFile(byte[] content, string fileExtension, string container, string currentRoute,
            string contentType);

    }
}
