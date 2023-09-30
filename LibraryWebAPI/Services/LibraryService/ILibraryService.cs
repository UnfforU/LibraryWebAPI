namespace LibraryWebAPI.Services.LibraryService
{
    public interface ILibraryService
    {
        Task<List<Library>> GetAllLibraries();
        Task<Library> GetLibraryById(Guid guid);
        Task<List<Library>> AddLibrary(Library library);
        Task<List<Library>> UpdateLibrary(Guid guid, Library request);
        Task<List<Library>> DeleteLibrary(Guid guid);

    }
}
