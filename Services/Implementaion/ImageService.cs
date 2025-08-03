using CompanyPortal.Services.Abstractions;

namespace CompanyPortal.Services.Implementaion
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;

        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string?> SaveImageAsync(IFormFile file, string folderPath)
        {
            if (file == null || file.Length == 0)
                return null;


            var uploadsFolder = Path.Combine(_env.WebRootPath ?? Directory.GetCurrentDirectory(), folderPath);

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine(folderPath, fileName).Replace("\\", "/");
        }

        public async Task<string?> UpdateAsync(IFormFile newFile, string oldFilePath, string folder)
        {
            await DeleteAsync(oldFilePath);
            return await SaveImageAsync(newFile, folder);
        }

        public async Task<bool> DeleteAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            var fullPath = Path.Combine(_env.WebRootPath ?? Directory.GetCurrentDirectory(), filePath.TrimStart('/'));

            if (File.Exists(fullPath))
            {
                await Task.Run(() => File.Delete(fullPath));
                return true;
            }
            return false;

        }
    }
}
