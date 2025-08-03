
namespace CompanyPortal.Services.Abstractions
{
    public interface IImageService
    {
        Task<bool> DeleteAsync(string filePath);
        Task<string?> SaveImageAsync(IFormFile file, string folderPath);
        Task<string?> UpdateAsync(IFormFile newFile, string oldFilePath, string folder);
    }
}
