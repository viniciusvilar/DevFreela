using DevFreela.API;
using Microsoft.AspNetCore.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.AddJsonFiles(builder.Configuration, builder.Environment);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

/*using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}*/


startup.Configure(app, app.Environment);

app.Run();
