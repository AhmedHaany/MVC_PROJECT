namespace Demo.PresentationLayer.Utilities
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName) 
        {
            // C:\Users\ahmed\Source\Repos\AhmedHaany\MVC_PROJECT\Demo.PresentationLayer\wwwroot\Files\Images\
            // string folderPAth = Directory.GetCurrentDirectory()+@"\wwwroot\Files\";

            string folderPAth = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\Files", folderName);

            string fileName = $"{Guid.NewGuid}-{file.FileName}";

            string filePath = Path.Combine(folderPAth, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(stream);

            return fileName;
        }

        public static void DeleteFile(string folderName ,string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName,fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

        }

    }
}
