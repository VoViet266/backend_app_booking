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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HisDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocalConnection")));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var jwtConfig  = builder.Configuration.GetSection("Jwt");
var secretKey  = jwtConfig["SecretKey"]  ?? throw new InvalidOperationException("Thiếu Jwt:SecretKey trong appsettings");
var issuer     = jwtConfig["Issuer"]     ?? "his-backend";
var audience   = jwtConfig["Audience"]  ?? "his-app";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuer           = true,
            ValidIssuer              = issuer,
            ValidateAudience         = true,
            ValidAudience            = audience,
            ValidateLifetime         = true,
            ClockSkew                = TimeSpan.Zero   // Không cho phép trễ giờ
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


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{   
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title       = "HIS Backend API",
        Version     = "v1",
        Description = "API hệ thống thông tin bệnh viện"
    });

    // Thêm nút Authorize trong Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name         = "Authorization",
        Type         = SecuritySchemeType.Http,
        Scheme       = "Bearer",
        BearerFormat = "JWT",
        In           = ParameterLocation.Header,
        Description  = "Nhập JWT token. Ví dụ: Bearer {token}"
    });
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HIS API v1");
        c.RoutePrefix = "swagger";  // Truy cập tại /swagger
    });
}

app.UseCors("AllowAll");
app.UseAuthentication();   
app.UseAuthorization();
app.MapControllers();

app.Run();
