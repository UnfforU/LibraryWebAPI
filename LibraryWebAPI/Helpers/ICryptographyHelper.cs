namespace LibraryWebAPI.Helpers
{
    public interface ICryptographyHelper
    {
        string ComputeSHA256(string input, string salt);
        string GenerateSalt();
        string GenerateJWT(User user);
    }
}
