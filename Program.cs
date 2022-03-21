using Newtonsoft.Json.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<MyGetFilesInfo>();

var app = builder.Build();

app.MapGet("/browse", (string? path, string? group, MyGetFilesInfo myGetFilesInfo) =>
{
    IEnumerable<MyFileData> files = myGetFilesInfo.GetFiles(path);

    if (group == "true")
    {
        JArray group_query = new JArray(
            from file in files
            select new JObject(
                new JProperty("type", file.FileType),
                new JProperty("files",
                    new JArray(
                        from f in files
                        where f.FileType == file.FileType
                        select new JObject(
                            new JProperty("Name", file.Name),
                            new JProperty("LastMod", file.LastMod),
                            new JProperty("Size", file.Size)
                        )))));
            
        return group_query.ToString();
    }

    JArray query = new JArray(
        from file in files
        select new JObject(
            new JProperty("Name", file.Name),
            new JProperty("LastMod", file.LastMod),
            new JProperty("Size", file.Size)
        ));

    return query.ToString();
});

app.Run();
