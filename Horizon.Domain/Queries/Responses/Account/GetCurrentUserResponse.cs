namespace Horizon.Domain.Queries.Responses.Account;

public class GetCurrentUserResponse
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? NickName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string? ProfileImageUrl { get; set; }
    public bool Verified { get; set; }
    public DateTime? Birthdate { get ; set; }
}
