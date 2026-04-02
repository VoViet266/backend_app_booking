namespace his_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
[Table("app_usertoken", Schema = "datlichkham")]
public class Usertoken
{   
    public int UserId { get; set; }
    public string? DeviceId { get; set; }
    public string? FcmToken { get; set; }
    public string? RefreshToken { get; set; } 
    public DateTimeOffset? RefreshTokenHetHan { get; set; }
}