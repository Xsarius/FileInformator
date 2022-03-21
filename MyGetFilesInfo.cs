public class MyGetFilesInfo
{
    public IEnumerable<MyFileData> GetFiles(string path)
    {
        if (path == "" || path == null)
            return Enumerable.Empty<MyFileData>();
        
        List<MyFileData> all_files = new List<MyFileData>();
        
        string[] dir_paths = Directory.GetDirectories(path);    

        foreach(string dir_path in dir_paths)
        {
            all_files.AddRange(GetFilesInDir(dir_path));
        }

        all_files.AddRange(GetFilesInDir(path));

        return all_files.AsEnumerable();
    }

    private List<MyFileData> GetFilesInDir(string path)
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

            return files;
        }
        catch (Exception ex)
        {
           Console.WriteLine(ex.ToString());
        }

        return files;
    }
}