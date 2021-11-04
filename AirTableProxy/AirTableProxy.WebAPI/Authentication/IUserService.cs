using System.Threading.Tasks;

namespace AirTableProxy.WebAPI.Authentication
{
    public interface IUserService
    {
        public Task<UserEntity> ValidateCredentials(string username, string password);
    }
}
