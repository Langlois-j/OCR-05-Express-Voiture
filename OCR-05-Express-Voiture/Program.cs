using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Data.Seeders;  // ✅ Import
using OCR_05_Express_Voiture.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ✅ User Secrets sont chargés automatiquement en Development
// Pas besoin de config supplémentaire - ils sont dans IConfiguration

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICarBrandRepository, CarBrandRepository>();

// ✅ Enregistrer AdminSeeder
builder.Services.AddScoped<AdminSeeder>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity avec rôles
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()  // ✅ Important pour les rôles
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// ✅ SEEDING AVEC ADMIN
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        logger.LogInformation("🔄 Application des migrations...");
        db.Database.Migrate();

        // ✅ Seeding admin depuis User Secrets
        logger.LogInformation("🔐 Seeding du compte admin...");
        var adminSeeder = services.GetRequiredService<AdminSeeder>();
        await adminSeeder.SeedAdminAsync();

        logger.LogInformation("✅ Seeding terminé");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "❌ Erreur lors du seeding");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

public partial class Program { }