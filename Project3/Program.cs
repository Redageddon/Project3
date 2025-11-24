using Project3.Services;

namespace Project3;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Register HttpClient for API calls
        builder.Services.AddHttpClient("RecipeAPI", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5000");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        // Register API services
        builder.Services.AddScoped<RecipeApiService>();
        builder.Services.AddScoped<UserApiService>();
        builder.Services.AddScoped<PlannerApiService>();
        builder.Services.AddScoped<AuthApiService>();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute("default",
                               "{controller=Home}/{action=Index}/{id?}")
           .WithStaticAssets();

        app.Run();
    }
}