namespace AdminDashBoardV1._0._0.Helper
{
    public class PictureResolver
    {
        public static string Resolve(IFormFile file, string folderName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);

            // Ensure directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // unique file name
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

            // get file path
            var filePath = Path.Combine(folderPath, uniqueFileName);

            // save the file to the server as string 
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine("images/products", uniqueFileName); // for src attribute in <img>
        }

        public static void Delete(string filePath)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
