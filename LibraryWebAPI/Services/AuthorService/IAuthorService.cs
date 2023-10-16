namespace LibraryWebAPI.Services.AuthorService
{
    public interface IAuthorService
    {
        Task<List<AuthorDTO>> GetAuthorListAsync();
        Task<AuthorDTO> AddAuthorAsync(AuthorDTO author);
        Task<bool> DeleteAuthorAsync(Guid id);

    }
}
