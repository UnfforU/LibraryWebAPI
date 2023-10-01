namespace LibraryWebAPI.Services.LibraryService
{
    public interface ILibraryService
    {
        Task<List<Library>> GetAllLibraries();
        Task<Library?> GetLibraryById(Guid guid);
        Task<Library?> AddLibrary(Library library);
        Task<Library?> UpdateLibrary(Guid guid, Library request);
        Task<List<Library>> DeleteLibrary(Guid guid);

    }
}
