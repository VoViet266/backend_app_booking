public class DotKhamDto
{
    public required string Makb { get; set; }
    public required string kqchuandoan { get; set; }
    public  DateTime? Ngaydk { get; set; }
    public required List<DonthuocDto> Donthuocs { get; set; }
}
public class DonthuocDto    
{
    public required string Mahh { get; set; } 
    public required string Tenhh { get; set; } 

    public required string Tenhc {get; set;} 

    public required string Dvt { get; set; } 

    public decimal? Soluong { get; set; }

    public string? Cachuong { get; set; } 

}



public class LichSuKhamResponse
{
    public required string Mabn { get; set; } 
    public required string Makb { get; set; } 
    public required string Holot { get; set; } 
    public required string Ten { get; set; } 
    public DateOnly? Ngaysinh { get; set; }
    public decimal? Gioitinh { get; set; }
    public string? Ngaydangky { get; set; }
}   