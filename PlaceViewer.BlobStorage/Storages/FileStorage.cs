using Azure.Storage.Blobs;
using PlaceViewer.BusinessLogic.Interfaces.Storages;

namespace PlaceViewer.BlobStorage.Storages;

public class FileStorage : IFileStorage
{
    private readonly BlobContainerClient _containerClient;

    public FileStorage(BlobContainerClient containerClient)
    {
        ArgumentNullException.ThrowIfNull(containerClient);

        _containerClient = containerClient;
    }

    public async Task<string> SaveFile(Stream stream)
    {
        var client = _containerClient.GetBlobClient($"{Guid.NewGuid()}.jpg");
        await client.UploadAsync(stream);

        return client.Uri.AbsoluteUri;
    }
}