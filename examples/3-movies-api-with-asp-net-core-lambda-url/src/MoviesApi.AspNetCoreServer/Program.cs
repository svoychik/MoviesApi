var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Hello from AWS Lambda!");
app.MapGet("/movies", () => new List<Movie>()
{
    new Movie() { Id = 1, Name = "Movie1" },
    new Movie() { Id = 2, Name = "Movie2" }
});
app.MapGet(
    "/movies/{id}", 
    (int id) => new Movie() { Name = "Movie1", Id = id }
);

app.MapPost(
    "/movies",
    (Movie movie) => movie
);

app.Run();

public class Movie
{
    public int Id { get; set; }
    public string Name { get; set; }
}