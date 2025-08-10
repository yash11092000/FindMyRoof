namespace PhysioWeb.Repository
{
    public class FileUploadService
    {
        private readonly IWebHostEnvironment _environment;

        public FileUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadFile(IFormFile file, string baseFolder, string subFolder)
        {
            if (file == null || file.Length == 0)
                return null;

            // Validate file extension
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".doc", ".docx" };
            var fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!validExtensions.Contains(fileExt))
                throw new InvalidOperationException("Invalid file type");

            const int maxFileSize = 5 * 1024 * 1024; // 5MB
            if (file.Length > maxFileSize)
                throw new InvalidOperationException("File size exceeds limit");

            // Create directory structure
            var uploadPath = Path.Combine(_environment.WebRootPath, "SecureUploads", baseFolder, subFolder);
            Directory.CreateDirectory(uploadPath);

            // Generate unique filename
            var fileName = $"{Guid.NewGuid()}{fileExt}";
            var filePath = Path.Combine(uploadPath, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path
            return Path.Combine("SecureUploads", baseFolder, subFolder, fileName).Replace("\\", "/");
        }
    }
}
