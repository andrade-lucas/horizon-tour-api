namespace Horizon.Api.Controllers.Requests.Account;

public record UpdateAccountRequest(
    string FirstName,
    string LastName,
    string NickName,
    string Phone,
    DateTime Birthdate
);
