using Newtonsoft.Json;

public class MyFileData 
{
    private string? name;
    private string? last_mod;
    private string? file_type;
    private long size;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string LastMod
    {
        get { return last_mod; }
        set { last_mod = value; }
    }

    public long Size
    {
        get { return size; }
        set { size = value; }
    }

    public string FileType
    {
        get { return file_type;}
        set { file_type = value; }
    }

}