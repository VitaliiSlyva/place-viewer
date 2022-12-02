using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using PlaceViewer.BlobStorage.Storages;
using PlaceViewer.BusinessLogic.Interfaces.Services;
using PlaceViewer.BusinessLogic.Interfaces.Storages;
using PlaceViewer.BusinessLogic.Services;
using PlaceViewer.DatabaseStorage.Infrastructure;
using PlaceViewer.DatabaseStorage.Storages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPlaceService, PlaceService>();
builder.Services.AddScoped<IFileStorage, FileStorage>();
builder.Services.AddScoped<IPlaceStorage, PlaceStorage>();

builder.Services.AddDbContext<PlaceViewerDbContext>
(
    options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"))
);

builder.Services.AddSingleton
    (_ => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureConnectionString")));
builder.Services.AddSingleton<BlobContainerClient>
(
    provider => provider.GetService<BlobServiceClient>()!.GetBlobContainerClient
        (builder.Configuration.GetConnectionString("BlobContainer"))
);

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();