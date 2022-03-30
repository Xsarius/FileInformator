var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<MyGetFilesInfo>();

var app = builder.Build();

app.MapGet("/browse", (HttpRequest request, MyGetFilesInfo myGetFilesInfo) =>
{

    string path = request.Query["path"];
    bool group = Convert.ToBoolean(request.Query["group"]);

    if (group)
    {
        return myGetFilesInfo.GetGroupedFiles(path);
    }

    return myGetFilesInfo.GetUngroupedFiles(path);
});

app.Run();
