public class MyGetFilesInfo
{
    public IEnumerable<MyFileData> GetFiles(string path)
    {
        if (path == "" || path == null)
            return Enumerable.Empty<MyFileData>();

        List<MyFileData> files = new List<MyFileData>();

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
}