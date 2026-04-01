namespace his_backend.Common;

public class ServiceResult<T>
{
    public bool   Success    { get; set; }
    public string Message    { get; set; } = null!;
    public T?     Data       { get; set; }
    public int    StatusCode { get; set; }  

    public static ServiceResult<T> Ok(T data, string message = "Thành công", int code = 200) =>
        new() { Success = true,  Data = data,    Message = message, StatusCode = code };

    public static ServiceResult<T> Fail(string message, int code = 400) =>
        new() { Success = false, Data = default, Message = message, StatusCode = code };
}