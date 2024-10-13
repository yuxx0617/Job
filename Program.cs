using System.Text;
using Job.AppDBContext;
using Job.Dao;
using Job.Dao.Interface;
using Job.Service;
using Job.Service.Interface;
using Job.util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 啟用控制器
builder.Services.AddControllers();

// 啟用 HTTP 上下文存取
builder.Services.AddHttpContextAccessor();

// CORS 設定
var corsOrigins = builder.Configuration.GetSection("Cors:AllowOrigin").Value?.Split(',', StringSplitOptions.RemoveEmptyEntries);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        if (corsOrigins != null && corsOrigins.Contains("*"))
        {
            policy.SetIsOriginAllowed(_ => true); // 允許所有來源
        }
        else if (corsOrigins != null)
        {
            policy.WithOrigins(corsOrigins); // 允許特定的來源
        }
        policy.AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // 允許發送憑證
    });
});

// 配置資料庫
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDB")));

// 配置 JWT 認證
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "Job", // JWT 發行者

            ValidateAudience = false, // 不驗證接收者

            ValidateLifetime = true, // 驗證是否過期

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ababababab@cdcdcdcdcd@efefefefef")) // 密鑰
        };
    });

// 配置 appSettings 從配置文件中讀取
builder.Services.Configure<appSetting>(builder.Configuration.GetSection("appSettings"));

//配置 
builder.Services.AddHttpClient<ExternalApiService>();

// 註冊服務層
builder.Services.AddScoped<Token>();
builder.Services.AddScoped<TestService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IUserAnswerService, UserAnswerService>();

// 註冊 DAO 層
builder.Services.AddScoped<IUserDao, UserDao>();
builder.Services.AddScoped<ITestDao, TestDao>();
builder.Services.AddScoped<IUserAnswerDao, UserAnswerDao>();

// 配置 Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// 在開發模式下啟用 Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));
}

// 啟用 HTTPS 重導向
app.UseHttpsRedirection();

// 啟用靜態文件支援
var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
if (!Directory.Exists(wwwrootPath))
{
    Directory.CreateDirectory(wwwrootPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(wwwrootPath),
    RequestPath = "/static" // 靜態資源的路徑
});

// 啟用 CORS
app.UseCors("AllowAll");

// 啟用認證與授權中介
app.UseAuthentication();
app.UseAuthorization();

// 設定路由映射
app.MapControllers();

app.Run();