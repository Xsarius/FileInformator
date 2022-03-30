/// <summary>
///  Class used for temporarly storing selected file metadata
/// </summary>
public class MyFileData
{
    // File Name
    public string Name { get; set; }
    
    // File's last modification date
    public string LastMod { get; set; }

    // File size
    public long Size { get; set; }

    // File extension
    public string Extension { get; set; }

}