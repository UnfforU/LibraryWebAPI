namespace LibraryWebAPI.Services.UserRoleService
{
    public interface IUserRoleService
    {
        UserRoleDTO AddUserRole(UserRoleDTO author);
        bool DeleteUserRole(Guid id);
    }
}
