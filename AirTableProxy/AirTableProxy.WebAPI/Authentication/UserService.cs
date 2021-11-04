using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirTableProxy.WebAPI.Authentication
{
    public class UserService : IUserService
    {
        private List<UserEntity> _users = new()
        {
            new UserEntity { UserName = "test", Password = "test" }
        };

        public async Task<UserEntity> ValidateCredentials(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.UserName == username && x.Password == password));
            return user;
        }
    }
}
