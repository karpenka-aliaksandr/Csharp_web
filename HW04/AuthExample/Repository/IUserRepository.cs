using AuthExample.Model;

namespace AuthExample.Repository;

public interface IUserRepository
{
    public void UserAdd(string email, string password, Role role);
    public Role UserCheck(string name, string password);
}