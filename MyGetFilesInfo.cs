/// <summary>
///  Class used to obtain specific metadata of all files under specific path
/// </summary>
public class MyGetFilesInfo
{
    /// <summary>
    /// Method searching information about files under specific path and all levels down subfolders
    /// with no grouping by extension.
    /// </summary>
    /// <param name = "path"> Search path specification.</param>
    /// <returns>List of custom class files data.</returns> 
    public List<OutMyFileData> GetUngroupedFiles(string path)
    {
        return (
            from file in GetFiles(path)
            select new OutMyFileData
            {
                Name = file.Name,
                LastMod = file.LastMod,
                Size = file.Size,
            }).ToList();
    }
    /// <summary>
    /// Method searching information about files under specific path and all levels down subfolders
    /// grouped by extension.
    /// </summary>
    /// <param name = "path"> Search path specification.</param>
    /// <returns>List of custom class files data.</returns> 
    public List<OutMyFileData> GetGroupedFiles(string path)
    {
        return GetFiles(path).OrderBy(file => file.Extension)
                       .Select(file => new OutMyFileData
                       {
                           Name = file.Name,
                           LastMod = file.LastMod,
                           Size = file.Size,
                       }).ToList();
    }
    /// <summary>
    /// Method searching information about files under specific path and all levels down subfolders.
    /// No formating, contains information about file extension.
    /// </summary>
    /// <param name = "path"> Search path specification.</param>
    /// <returns>List of custom class files data.</returns> 
    private List<MyFileData> GetFiles(string path)
    {
        // Return empty list if path is null
        if (path == null)
            return new();

        // List of all files in given path folder and all subfolders
        List<MyFileData> allFiles = new();

        // Paths to subfolder in current folder
        string[] dirPaths = Directory.GetDirectories(path);

        // Paths to subfolders in current subfolder
        string[] tempDir;

        // Read files from subfolder one level down
        foreach (string dirPath in dirPaths)
        {
            try
            {
                // Get subfolders in subfolder
                tempDir = Directory.GetDirectories(dirPath);
            }
            catch
            {
                continue;
            }

            // Add files from subfolders below one level down subfolder
            foreach (string file in tempDir)
            {
                try
                {
                    allFiles.AddRange(GetFiles(file));
                }
                catch
                {
                    continue;
                }

            }

            // Add files from current subfolder
            allFiles.AddRange(GetFilesInDir(dirPath));
        }

        // Add files from current folder
        allFiles.AddRange(GetFilesInDir(path));

        return allFiles;
    }
    /// <summary>
    /// Method searching information about files in given folder.
    /// </summary>
    /// <param name = "path"> Search path specification.</param>
    /// <returns>List of custom class files data.</returns> 
    private static List<MyFileData> GetFilesInDir(string path)
    {
        // List of all files in given path
        List<MyFileData> files = new();

        // Read files in current folder
        string[] paths = Directory.GetFiles(path);

        // Add all files in current dir
        foreach (string tempPath in paths)
        {
            try
            {
                FileInfo file = new(tempPath);

                // Add current file custom information 
                files.Add(new MyFileData
                {
                    Name = file.Name,
                    LastMod = file.LastWriteTime.ToString(),
                    Size = file.Length,
                    Extension = file.Extension
                }); ;
            }
            catch
            {
                continue;
            }
        }

        return files;
    }
}