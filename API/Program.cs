using API.Services;

namespace API;

public partial class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<RecipeRepository>();
        builder.Services.AddSingleton<PlannerRepository>();
        builder.Services.AddSingleton<UserRepository>();
        builder.Services.AddSingleton<MealsRepository>();
        builder.Services.AddSingleton<PasswordHasher>();
        builder.Services.AddSingleton<SessionService>();
        builder.Services.AddScoped<UserAuth>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowMvcApp", policy =>
            {
                policy.WithOrigins("http://localhost:7297", "http://localhost:5297")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        WebApplication app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseCors("AllowMvcApp");
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}