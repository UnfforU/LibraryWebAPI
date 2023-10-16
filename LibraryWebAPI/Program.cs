using LibraryWebAPI.Services.AuthorBookService;
using LibraryWebAPI.Services.AuthorService;
using LibraryWebAPI.Services.AuthService;
using LibraryWebAPI.Services.BookService;
using LibraryWebAPI.Services.LibraryService;
using LibraryWebAPI.Services.OrderService;
using LibraryWebAPI.Services.UserRoleService;
using LibraryWebAPI.Services.UserService;
using LibraryWebAPI.Models.DB;
using LibraryWebAPI.Common;
using LibraryWebAPI.Helpers;


using Microsoft.AspNetCore.Authentication.JwtBearer;
using LibraryWebAPI.Services.FileLoaderService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Auth"));

var authOptions = builder.Configuration.GetSection("Auth").Get<AuthOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = authOptions.Audience,

            ValidateLifetime = true,

            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IFileLoaderService, FileLoaderService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAuthorBookService,  AuthorBookService>();
builder.Services.AddScoped<ICryptographyHelper, CryptographyHelper>();

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddDbContext<WebLibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
