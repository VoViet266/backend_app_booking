public class DotKhamDto
{
    public string Makb { get; set; }
    public string kqchuandoan { get; set; }
    public DateTime? Ngaydk { get; set; }
    public List<DonthuocDto> Donthuocs { get; set; }
}
public class DonthuocDto    
{
    public string Mahh { get; set; } = null!;
    public string Tenhh { get; set; } = null!;

    public string Tenhc {get; set;} = null!;

    public string Dvt { get; set; } = null!;

    public decimal? Soluong { get; set; }

    public string Cachuong { get; set; } 

}



public class LichSuKhamResponse
{
    public string Mabn { get; set; } = null!;
    public string Makb { get; set; } = null!;
    public string Holot { get; set; } = null!;
    public string Ten { get; set; } = null!;
    public DateOnly? Ngaysinh { get; set; }
    public decimal? Gioitinh { get; set; }
    public string? Ngaydangky { get; set; }
}   