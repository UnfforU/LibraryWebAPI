namespace LibraryWebAPI.Services.LibraryService
{
    public interface ILibraryService
    {
        Task<List<Library>> GetAll();
        Library GetById(Guid guid);
        Task<List<Library>> AddLibrary(Library library);
        List<Library> UpdateLibrary(Guid guid, Library library);
        List<Library> DeleteLibrary(Guid guid);

    }
}
