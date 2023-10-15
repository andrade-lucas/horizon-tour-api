namespace Horizon.Domain.Queries.Responses.Users;

public class GetAllUsersResponse
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public bool Verified { get; set; }
    public string ProfileImageUrl { get; set; }
}
