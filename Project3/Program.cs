using Project3.Services;

namespace Project3;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Session backing store (in-memory)
        builder.Services.AddDistributedMemoryCache();

        // Session configuration
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        // Allow services/controllers to access HttpContext/Session
        builder.Services.AddHttpContextAccessor();

        // Register HttpClient for API calls
        builder.Services.AddHttpClient("RecipeAPI", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5000");
            //client.BaseAddress = new Uri("https://####.ngrok-free.app"); // ngrok here for presentation
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

        // 🔹 Enable Session middleware (must be before Authorization)
        app.UseSession();

        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute("default",
                               "{controller=Home}/{action=Index}/{id?}")
           .WithStaticAssets();

        app.Run();
    }
}
