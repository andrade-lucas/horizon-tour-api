namespace Horizon.Auth.Queries.Responses;

public class GetUserByEmailResponse
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ProfileImageUrl { get; set; }
    public bool Verified { get; set; }
}
