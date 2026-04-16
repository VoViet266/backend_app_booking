namespace his_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
[Table("lichtrucbenhvien", Schema = "datlichkham")]
public partial class Lichtruc 
{
    public int? id { get; set; }
    public DateOnly? ngaytruc { get; set; }
    public string? manv { get; set; }
    public string? tenbacsi { get; set; }
    public int? loai_truc { get; set; }
}   