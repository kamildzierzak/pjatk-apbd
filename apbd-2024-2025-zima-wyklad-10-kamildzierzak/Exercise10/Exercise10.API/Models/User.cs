namespace Exercise10.API.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Login { get; set; }

    public string PasswordHash { get; set; }

    public string Salt { get; set; }

    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
