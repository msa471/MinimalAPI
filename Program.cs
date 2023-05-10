using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using MinimalAPI.Models;
using MinimalAPI.Services;
using System.Data;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using MinimalAPI.Repositories;
using MinimalAPI.DataLinkLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MinimalAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement 
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                { 
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<String>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,   
        ValidateLifetime = true,    
        ValidateIssuerSigningKey = true,    
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization(); 

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSingleton<IMovieService,MovieService>();     
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnectionString"];

builder.Services.AddDbContext<UserDbContext>(x => x.UseSqlServer(connectionString));


/*
var SqlConnection = builder.Configuration["ConnectionStrings:DefaultConnectionString"];

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(SqlConnection));*/

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
}); 

var app = builder.Build();
app.UseCors("corsapp");
app.UseSwagger();
app.UseAuthorization();
app.UseAuthentication();

app.MapGet("/", () => "Hello World!");
    

app.MapPost("/register",
(UserRegister user, IUserService service) => Register(user, service));

app.MapPost("/login",
(UserLogin user, IUserService service) => Login(user, service));


app.MapPost("/create",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
(Movie movie, IMovieService service) => Create(movie, service));

app.MapGet("/get",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard,Administrator")]
(int id, IMovieService service) => Get(id, service));

app.MapGet("/list",
(IMovieService service) => List(service));

app.MapPut("/update",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
(Movie newMovie, IMovieService service) => Update(newMovie, service));

app.MapDelete("/delete",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
(int id, IMovieService service) => Delete(id, service));


IResult Register(UserRegister user, IUserService service) {
    var registeredUser = service.GetRegistered(user);
    var userLogin = new UserLogin()
    {
        Username = registeredUser.Username,
        Password = registeredUser.Password
    };
    var token = getJWT(userLogin,service);

    return Results.Ok(new { token = token, username = registeredUser.Username});

}

string getJWT(UserLogin user, IUserService service)
{
    var loggedInUser = service.GetUsers(user);
    if (loggedInUser is null) return "";

    var claims = new[]
    {
            new Claim(ClaimTypes.NameIdentifier, loggedInUser.Username),
            new Claim(ClaimTypes.Email, loggedInUser.Email),
            new Claim(ClaimTypes.Role, loggedInUser.Role),
        };

    var token = new JwtSecurityToken
    (
        issuer: builder.Configuration["Jwt:Issuer"],
        audience: builder.Configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddDays(60),
        notBefore: DateTime.UtcNow,
        signingCredentials: new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            SecurityAlgorithms.HmacSha256)
    );

    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
    
    return tokenString;

//    throw new NotImplementedException();
}

IResult Login(UserLogin user, IUserService service)
{
    if (!string.IsNullOrEmpty(user.Username) &&
        !string.IsNullOrEmpty(user.Password))
    {
        var tokenString = getJWT(user,service);

        return Results.Ok(tokenString);
    }
    return Results.BadRequest(Results.NotFound());
}

IResult Create(Movie movie, IMovieService service) 
{
    var result = service.Create(movie);
    return Results.Ok(result);
}

IResult Get(int id, IMovieService service)
{ 
    var movie = service.Get(id);

    if (movie is null) return Results.NotFound("Movie not found!");
    
    return Results.Ok(movie);       
}

IResult List(IMovieService service)
{
    var movies = service.List();

    return Results.Ok(movies);
}

IResult Update(Movie newMovie, IMovieService service)
{ 
    var updatedMovie = service.Update(newMovie);

    if (updatedMovie is null) return Results.NotFound("Movie not found!");      

    return Results.Ok(updatedMovie);    
}

IResult Delete(int id, IMovieService service)
{
    var result = service.Delete(id);

    if (!result) Results.BadRequest("Something went wrong!");

    return Results.Ok(result);
}

AppDbInitializer.Seed(app);

app.UseSwaggerUI();
 
app.Run();
