namespace PlaceViewer.BusinessLogic.Interfaces.Storages;

public interface IFileStorage
{
    Task<string> SaveFile(Stream stream);
}