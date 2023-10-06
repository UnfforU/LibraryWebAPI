using AutoMapper;

namespace LibraryWebAPI.Services.UserRoleService
{
    public class UserRoleService : IUserRoleService
    {
        private readonly WebLibraryDbContext _context;
        private readonly IMapper _mapper;
        public UserRoleService(WebLibraryDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public UserRoleDTO AddUserRole(UserRoleDTO userRole)
        {
            var newUserRole = _mapper.Map<UserRole>(userRole);

            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();

            return _mapper.Map<UserRoleDTO>(newUserRole);
        }
        public bool DeleteUserRole(Guid id)
        {
            var deletedUserRole = _context.UserRoles.Find(id);
            if (deletedUserRole != null)
            {
                _context.UserRoles.Remove(deletedUserRole);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
