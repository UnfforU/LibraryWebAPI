using LibraryWebAPI.Models.DB;

namespace LibraryWebAPI.Services.LibraryService
{
    public interface ILibraryService
    {
        Task<List<LibraryDTO>> GetLibrariesAsync();
        Task<LibraryDTO> GetLibraryByIdAsync(Guid id);
        Task<LibraryDTO> AddLibraryAsync(LibraryDTO library);
        Task<LibraryDTO> UpdateLibraryAsync(Guid id, LibraryDTO library);
        Task<bool> DeleteLibraryAsync(Guid id);

    }
}
