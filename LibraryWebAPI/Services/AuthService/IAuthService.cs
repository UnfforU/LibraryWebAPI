namespace LibraryWebAPI.Services.AuthService
{
    public interface IAuthService
    {
        string AuthenticateUser(LoginDTO login);

    }
}
