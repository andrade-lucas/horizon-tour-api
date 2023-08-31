namespace Horizon.Auth.Queries.Responses;

public class GetUserAuthResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string ProfileImageUrl { get; set; }
    public bool Verified { get; set; }
}
