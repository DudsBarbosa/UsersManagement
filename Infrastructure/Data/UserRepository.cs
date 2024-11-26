using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        User IUserRepository.CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        void IUserRepository.DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        User IUserRepository.GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<User> IUserRepository.GetUsers()
        {
            throw new NotImplementedException();
        }

        User IUserRepository.UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
