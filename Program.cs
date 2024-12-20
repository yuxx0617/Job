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

// 添加 Razor Pages 和 MVC 控制器支援
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173") // 允許前端的來源
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // 如果你需要傳遞驗證憑證，如 cookies 或 HTTP 身份驗證
        });
});


// 配置資料庫
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDB")));

builder.Services.Configure<appSetting>(builder.Configuration.GetSection("appSettings"));

// 註冊 Service 層
builder.Services.AddScoped<Token>();
builder.Services.AddScoped<TestService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IUserAnswerService, UserAnswerService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IPredictService, PredictService>();

// 註冊 DAO 層
builder.Services.AddScoped<IUserDao, UserDao>();
builder.Services.AddScoped<ITestDao, TestDao>();
builder.Services.AddScoped<IUserAnswerDao, UserAnswerDao>();
builder.Services.AddScoped<IJobDao, JobDao>();
builder.Services.AddScoped<IPredictDao, PredictDao>();

builder.Services.AddHttpClient();

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

// 配置 JWT 驗證
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // 使用 JWT Bearer 驗證方案
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true; // 在驗證失敗時，回應中包含錯誤詳細資訊

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // 驗證 Token 的發行者
            ValidIssuer = "Job", // 定義合法的發行者

            ValidateAudience = false, // 不驗證 Token 的接受者

            ValidateLifetime = true, // 驗證 Token 是否在有效期內

            ValidateIssuerSigningKey = true, // 驗證簽章的密鑰

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ababababab@cdcdcdcdcd@efefefefef")) // 設定 JWT 簽章的密鑰
        };
    });

// Add authorization services
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("True"));
});

// 添加 Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// 檢查並建立 wwwroot 資料夾
var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
if (!Directory.Exists(wwwrootPath))
{
    Directory.CreateDirectory(wwwrootPath);
}

// 配置 HTTP 請求處理
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// 配置靜態文件服務
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(wwwrootPath),
    RequestPath = "/static"
});

app.UseCors("AllowSpecificOrigin");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// 啟用 Swagger 生成的 JSON 端點
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger";
});

app.MapRazorPages();
app.MapControllers();

app.Run();