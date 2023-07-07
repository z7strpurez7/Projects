using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using movieCharacterAPI.Context;
using movieCharacterAPI.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



//Add dbContext
builder.Services.AddDbContext<MovieContext>(option =>
//Getting connectionstring from appsettings.json

option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "MovieCharacter-API",
        Description = "Keep track of your movies and franchises",
        Contact = new OpenApiContact
        {
            Name = "Ali Todashev",
            Url = new Uri("https://github.com/z7strpurez7/Projects/tree/2d427aff34e8609afa93ca382750a54d17fd819f/Backend/movieCharacterAPI")
        }
    });
    options.IncludeXmlComments(xmlPath);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
//app.UseSwaggerUI();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));
app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();


//Seeds data by adding movies to the database
void SeedData(MovieContext _context)
{
    var F1 = new FranchiseEntity { Name = "Alien", Description = "The world of Aliens", Movies = new List<MovieEntity>() };
    var F2 = new FranchiseEntity { Name = "Predator", Description = "First interactions with Predators", Movies = new List<MovieEntity>() };

    // Seed Characters
    var char1 = new CharacterEntity { Name = "Ellen", Alias = null, Gender = "Female", Picture = "https://example.com/character1.jpg", Movies = null! };
    var char2 = new CharacterEntity { Name = "Arnold", Alias = "Terminatior", Gender = "Male", Picture = "https://example.com/character2.jpg", Movies = null! };
    var char3 = new CharacterEntity { Name = "Charles", Alias = null, Gender = "Male", Picture = "https://example.com/character3.jpg", Movies = null! };

    // Seed Movies
    var mov1 = new MovieEntity { Title = "Alien", Genre = "Action,horror, scifi", ReleaseYear = "1979", Director = "Ridley Scott", Picture = "https://play-lh.googleusercontent.com/tZsW2cETxSjdJk7RGW6hskzEHBjMGUhvbi7qG-Ae8nJMkGegbpMmE_GoCMLW8ROpgY4", Trailer = "https://youtube.com/trailer1", FranchiseId = 1, Character = new List<CharacterEntity>(), Franchise = F1 };
    var mov2 = new MovieEntity { Title = "The Predator", Genre = "Drama,horror, scifi", ReleaseYear = "1987", Director = "Shane Black", Picture = "https://example.com/movie2.jpg", Trailer = "https://youtube.com/trailer2", FranchiseId = 2, Character = new List<CharacterEntity>(), Franchise = F2 };
    var mov3 = new MovieEntity { Title = "Alien 2", Genre = "Drama, scifi, horror", ReleaseYear = "2022", Director = "Director 2", Picture = "https://example.com/movie2.jpg", Trailer = "https://youtube.com/trailer2", FranchiseId = 1, Character = new List<CharacterEntity>(), Franchise = F1 };
    var mov4 = new MovieEntity { Title = "Predator 2", Genre = "Drama,horror, scifi", ReleaseYear = "2022", Director = "Director 2", Picture = "https://example.com/movie2.jpg", Trailer = "https://youtube.com/trailer2", FranchiseId = 2, Character = new List<CharacterEntity>(), Franchise = F2 };

    //Add characters to movies
    mov1.Character.Add(char1);
    mov1.Character.Add(char3);
    mov2.Character.Add(char2);
    mov3.Character.Add(char1);
    mov4.Character.Add(char2);
    //add movies to context
    _context.Movies.Add(mov1);
    _context.Movies.Add(mov2);
    _context.Movies.Add(mov3);
    _context.Movies.Add(mov4);
    //saving changes to db
    _context.SaveChanges();
}