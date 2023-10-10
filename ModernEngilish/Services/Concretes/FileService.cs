using ModernEngilish.Contracts.File;
using ModernEngilish.Services.Abstracts;

namespace ModernEngilish.Services.Concretes
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }

        public async Task<string> UploadAsync(IFormFile formFile, UploadDirectory uploadDirectory)
        {
            string directoryPath = GetUploadDirectory(uploadDirectory);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var imageNameInFileSystem = GenerateUniqueFileName(formFile.FileName);

            var filePath = Path.Combine(directoryPath, imageNameInFileSystem);

            try
            {
                using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await formFile.CopyToAsync(fileStream);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Some Things Went Wrong");
                throw;
            }

            return imageNameInFileSystem;
        }

        public async Task DeleteAsync(string? fileName, UploadDirectory uploadDirectory)
        {
            var deletePath = Path.Combine(GetUploadDirectory(uploadDirectory), fileName!);

            await Task.Run(() => File.Delete(deletePath));
        }

        public string GetFileUrl(string? fileName, UploadDirectory uploadDirectory)
        {
            string initialSegment = "Client/custom-files/";

            switch (uploadDirectory)
            {
                case UploadDirectory.EngProgram:
                    return $"{initialSegment}/EngProgram/{fileName}";
                case UploadDirectory.Language:
                    return $"{initialSegment}/Language/{fileName}";
                case UploadDirectory.Aged:
                    return $"{initialSegment}/Aged/{fileName}";
                case UploadDirectory.Gallery:
                    return $"{initialSegment}/Gallery/{fileName}";
                case UploadDirectory.Career:
                    return $"{initialSegment}/Career/{fileName}";
                case UploadDirectory.Graduate:
                    return $"{initialSegment}/Graduate/{fileName}";
                default:
                    throw new Exception("Something went wrong");
            }
        }

        private string GenerateUniqueFileName(string fileName)
        {
            return $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        }

        private string GetUploadDirectory(UploadDirectory uploadDirectory)
        {
            string startPath = Path.Combine("wwwroot", "Client", "custom-files");

            switch (uploadDirectory)
            {
                case UploadDirectory.EngProgram:
                    return Path.Combine(startPath, "EngProgram");
                case UploadDirectory.Language:
                    return Path.Combine(startPath, "Language");
                case UploadDirectory.Aged:
                    return Path.Combine(startPath, "Aged");
                case UploadDirectory.Gallery:
                    return Path.Combine(startPath, "Gallery");
                case UploadDirectory.Career:
                    return Path.Combine(startPath, "Career");
                case UploadDirectory.Graduate:
                    return Path.Combine(startPath, "Graduate");
                default:
                    throw new Exception("Something went wrong");
            }
        }
    }
}
