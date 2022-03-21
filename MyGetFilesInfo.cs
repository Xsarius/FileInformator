public class MyGetFilesInfo
{
    public IEnumerable<MyFileData> GetFiles(string path)
    {
  
        List<MyFileData> files = new List<MyFileData>();

        try
        {
            string[] paths = Directory.GetFiles(path);

            foreach (string temp_path in paths)
            {
                FileInfo file = new FileInfo(temp_path);
                files.Add(new MyFileData
                {
                Name = file.Name,
                LastMod = file.LastWriteTime.ToString(),
                Size = file.Length,
                FileType = file.Extension
                });

            }

            return files.AsEnumerable();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return files;
    }
}