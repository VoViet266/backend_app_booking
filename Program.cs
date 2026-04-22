using his_backend.Models;
using his_backend.Services;
using his_backend.Services.chuyekhoa;
using his_backend.Services.dangkykham;
using his_backend.Integration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

using Google.Apis.Auth.OAuth2;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var jwtConfig = builder.Configuration.GetSection("Jwt");
var secretKey = jwtConfig["SecretKey"] ?? throw new InvalidOperationException("Thiếu Jwt:SecretKey trong appsettings");
var issuer = jwtConfig["Issuer"] ?? "his-backend";
var audience = jwtConfig["Audience"] ?? "his-app";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHoSoBenhNhanService, HoSoBenhNhanService>();
builder.Services.AddScoped<HoSoBenhNhanService>();
builder.Services.AddScoped<chuyenkhoaService>();
builder.Services.AddScoped<IBacsiService, BacsiService>();
builder.Services.AddScoped<DonthuocService>();
builder.Services.AddScoped<IDangkykbService, DangkykbService>();
builder.Services.AddScoped<IHis_BenhnhanIntegration, His_BenhnhanIntegration>();
builder.Services.AddScoped<IHis_BacsiIntegration, His_BacsiIntegration>();
builder.Services.AddScoped<ILichtrucService, LichtrucService>();
builder.Services.AddScoped<chinhanhService>();


builder.Services.AddRateLimiter(options =>
{
    // API thường
    options.AddTokenBucketLimiter("normal", opt =>
    {
        opt.TokenLimit = 100;
        opt.TokensPerPeriod = 50;
        opt.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
        opt.AutoReplenishment = true;
    });

    // Auth
    options.AddFixedWindowLimiter("auth", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromMinutes(1);
    });

    // Realtime nhẹ
    options.AddTokenBucketLimiter("realtime", opt =>
    {
        opt.TokenLimit = 30;
        opt.TokensPerPeriod = 10;
        opt.ReplenishmentPeriod = TimeSpan.FromSeconds(5);
    });
});


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});
builder.WebHost.ConfigureKestrel(options =>
{
    // Giới hạn kích thước body để tránh tấn công từ chối dịch vụ (DoS) với các request quá lớn
    options.Limits.MaxRequestBodySize = 5242880; // 5 MB
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
builder.Host.UseWindowsService();
var app = builder.Build();
app.UseRateLimiter();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
/// Đảm bảo giao tiếp qua kên mã hóa
app.UseHttpsRedirection();
app.MapControllers();

app.Run("http://0.0.0.0:8080");