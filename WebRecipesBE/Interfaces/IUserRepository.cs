using WebRecipesBE.Models;

namespace WebRecipesBE.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);//reikia paduoti entity o ne tik id !!!! id gausim per parametrus
        ICollection<User> GetAllUsers();
        bool UserExists(int id);

        bool Save();
    }
}
