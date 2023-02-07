using wallet.core.Entities;

namespace wallet.infrastructure.Services.UserService;

public interface IUserService
{
    Task Add(string name);
    Task<User> Get(int userId);

    Task<Dictionary<string, decimal>> GetBalance(int userId);
}

