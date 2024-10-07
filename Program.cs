using System.Text;
using Job.AppDBContext;
using Job.Dao;
using Job.Dao.Interface;
using Job.Service;
using Job.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
            builder =>
            {
                builder.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader();
            });
});

// builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

var corsOrigins = builder.Configuration.GetSection("Cors:AllowOrigin").Value.Split(',', StringSplitOptions.RemoveEmptyEntries);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                                    builder =>
                                    {
                                        if (corsOrigins.Contains("*"))
                                        {
                                            builder.SetIsOriginAllowed(_ => true);
                                        }
                                        else
                                        {
                                            builder.WithOrigins(corsOrigins);
                                        }
                                        builder.AllowAnyMethod();
                                        builder.AllowAnyHeader();
                                        builder.AllowCredentials();
                                    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDB")));

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


// Service
builder.Services.AddScoped<Token>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITestService, TestService>();

// Dao
builder.Services.AddScoped<IUserDao, UserDao>();
builder.Services.AddScoped<ITestDao, TestDao>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAll");
app.Run();


// var app = builder.Build();

// var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
// if (!Directory.Exists(wwwrootPath))
// {
//     Directory.CreateDirectory(wwwrootPath);
// }

// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     app.UseHsts();
// }

// app.UseHttpsRedirection();

// app.UseStaticFiles(new StaticFileOptions
// {
//     FileProvider = new PhysicalFileProvider(wwwrootPath),
//     RequestPath = "/static"
// });

// app.UseRouting();

// app.UseAuthentication();
// app.UseAuthorization();

// app.UseSwagger();

// app.UseSwaggerUI(c =>
// {
//     c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//     c.RoutePrefix = "swagger";
// });

// app.MapRazorPages();
// app.MapControllers();

// app.Run();
