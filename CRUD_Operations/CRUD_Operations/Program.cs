using CRUD_Operations.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryPatternWithUOW.Core.Interface;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.EF;
using RepositoryPatternWithUOW.EF.Repositories;
using CRUD_Operations.Filters;
using CRUD_Operations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CRUD_Operations.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("config.json");
builder.Services.Configure<AttachmentOptions>(builder.Configuration.GetSection("Attachments")); // option patterns
//var attachmentOptions = builder.Configuration.GetSection("Attachments").Get<AttachmentOptions>();
//builder.Services.AddSingleton(attachmentOptions); //way 1

//var attachmentOptions2 = new AttachmentOptions();
//builder.Configuration.GetSection("Attachments").Bind(attachmentOptions2);
//builder.Services.AddSingleton(attachmentOptions2);


// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<LogActfityFilters>();
    options.Filters.Add<PermassionBasedAuthorization>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddTransient(typeof(IBaseRepository<>),typeof(BaseRepository<>));
//
//builder.Services.AddAuthentication()
   // .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic",null);

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
builder.Services.AddSingleton(jwtSettings);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Adminonly", builder =>
    {
        builder.RequireRole("Admin", "SuperUser");
    });
});
builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.SaveToken = true; 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey)) //

        };
    });
builder.Services.AddAuthorization();
builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<ProfileIloggerMiddelware>();
app.UseHttpsRedirection();

app.UseAuthentication();
 app.UseAuthorization();

app.MapControllers();

app.Run();
