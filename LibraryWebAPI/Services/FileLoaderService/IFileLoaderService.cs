namespace LibraryWebAPI.Services.FileLoaderService
{
    public interface IFileLoaderService
    {
        Task<bool> UploadFileAsync(IFormFile file);
        Task<FileDTO> GetFileByBookId(Guid bookId);
    }
}
