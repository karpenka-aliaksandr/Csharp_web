using System.Security.Cryptography;
using System.Text;
using AuthExample.Context;
using AuthExample.Model;

namespace AuthExample.Repository
{
    public class UserRepository(/*IMapper mapper*/) : IUserRepository
    {
        //private readonly IMapper _mapper = mapper;

        public void UserAdd(string email, string password, Role role)
        {
            using (var context = new UserContext())
            {
                if (role == Role.Admin)
                {
                    if (context.Users.Any(x => x.RoleId == (int)Role.Admin))
                    {
                        throw new Exception("Admin already exists");
                    }
                }

                var user = new User()
                {
                    Email = email,
                    RoleId = (int)role,
                    Salt = new byte[16]
                };

                new Random().NextBytes(user.Salt);
                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray();

                SHA512 shaM = new SHA512Managed();
                user.Password = shaM.ComputeHash(data);
                context.Add(user);
                context.SaveChanges();
            }
        }

        public Role UserCheck(string name, string password)
        {
            using (var context = new UserContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Email == name);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray();
                SHA512 shaM = new SHA512Managed();
                var hash = shaM.ComputeHash(data);

                if (hash.SequenceEqual(user.Password))
                {
                    return Enum.Parse<Role>(user.RoleId.ToString());
                }

                throw new Exception("Wrong password");
            }
        }

    }
}
