namespace his_backend.Models;

using System.ComponentModel.DataAnnotations.Schema;
[Table("app_usertoken", Schema = "datlichkham")]
public class Usertoken
{   
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? DeviceId { get; set; }
    public string? FcmToken { get; set; }
    public string? RefreshToken { get; set; } 
    public DateTimeOffset? RefreshTokenHetHan { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public bool IsActive { get; set; } = true;
}