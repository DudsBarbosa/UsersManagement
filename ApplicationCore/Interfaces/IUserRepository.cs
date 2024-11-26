using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IUserRepository
    {
        public User CreateUser(User user);
        public User GetUser(int userId);
        public IEnumerable<User> GetUsers();
        public User UpdateUser(User user);
        public void DeleteUser(int userId);

    }
}
