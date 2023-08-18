using Horizon.Domain.Entities;
using Horizon.Domain.ValueObjects;

namespace Horizon.Auth.Repositories;

public class AuthRepository
{
    private List<User> _users;
    
    public AuthRepository()
    {
        FillDatabase();
    }
    
    public User GetByCredentials(string email, string password)
    {
        return _users.FirstOrDefault(
            x => x.Email.Address == email 
            && x.Password.Value == password
        );
    }

    private void FillDatabase()
    {
        this._users = new List<User>
        {
            new User
            (
                new Name("Teste", "Andrade", "teste_bot"),
                new Email("teste@gmail.com"),
                new Phone("99999999999"),
                new Password("teste123"),
                null
            )
        };
    }
}
