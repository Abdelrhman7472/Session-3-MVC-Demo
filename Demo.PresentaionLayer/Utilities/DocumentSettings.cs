namespace Demo.PresentaionLayer.Utilities
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //string folderPath = Directory.GetCurrentDirectory() + @"\wwwroot\Files";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files",folderName);// create folder path

            string fileName = $"{Guid.NewGuid()}-{file.FileName}"; // create file name

            string filePath=Path.Combine(folderPath,fileName);// create file path

            using var stream = new FileStream(filePath,FileMode.Create);//create file to save file

            file.CopyTo(stream); //copy file to fileStream

            return fileName;
         }
          
        public static void DeleteFile(string foldeName,string fileName) { 
        
            string filePath= Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", foldeName,fileName);

            if (File.Exists(filePath)) 
                File.Delete(filePath);
            
        
        }
    }
}
