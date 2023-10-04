using LibraryWebAPI.Models.DB;

namespace LibraryWebAPI.Services.LibraryService
{
    public interface ILibraryService
    {
        Task<List<Library>> GetAllLibraries();
        Task<Library?> GetLibraryById(Guid libraryId);
        Task<Library?> AddLibrary(Library library);
        Task<Library?> UpdateLibrary(Guid libraryId, Library request);
        Task<List<Library>> DeleteLibrary(Guid libraryId);

    }
}
