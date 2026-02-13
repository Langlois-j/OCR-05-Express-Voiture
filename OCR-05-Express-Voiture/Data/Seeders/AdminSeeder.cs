using Microsoft.AspNetCore.Identity;

namespace OCR_05_Express_Voiture.Data.Seeders
{
    /// <summary>
    /// Service pour seeder un compte admin de manière sécurisée
    /// Récupère les credentials depuis les User Secrets ou variables d'environnement
    /// </summary>
    public class AdminSeeder
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AdminSeeder> _logger;

        public AdminSeeder(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILogger<AdminSeeder> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Seede un compte admin si les credentials sont configurés et l'admin n'existe pas
        /// </summary>
        public async Task SeedAdminAsync()
        {
            try
            {
                // Récupère les credentials depuis User Secrets ou variables d'environnement
                var adminEmail = _configuration["Admin:Email"];
                var adminPassword = _configuration["Admin:Password"];

                // ✅ Sécurité : vérifier que les secrets sont configurés
                if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
                {
                    _logger.LogWarning("⚠️ Les credentials Admin ne sont pas configurés. Seed admin ignoré.");
                    return;
                }

                // ✅ Sécurité : vérifier que l'admin n'existe pas déjà
                var existingAdmin = await _userManager.FindByEmailAsync(adminEmail);
                if (existingAdmin != null)
                {
                    _logger.LogInformation("✓ Compte admin existant détecté. Seed admin ignoré.");
                    return;
                }

                // ✅ Sécurité : créer la role "Admin" si elle n'existe pas
                const string adminRole = "Admin";
                if (!await _roleManager.RoleExistsAsync(adminRole))
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole(adminRole));
                    if (!roleResult.Succeeded)
                    {
                        _logger.LogError("❌ Erreur lors de la création du rôle Admin");
                        return;
                    }
                    _logger.LogInformation("✓ Rôle Admin créé avec succès");
                }

                // ✅ Créer le compte admin
                var admin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,  // Email confirmé pour éviter les blocages
                    LockoutEnabled = false  // Admin ne peut pas être verrouillé (optionnel)
                };

                var result = await _userManager.CreateAsync(admin, adminPassword);
                if (!result.Succeeded)
                {
                    _logger.LogError("❌ Erreur lors de la création du compte admin : {Errors}",
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                    return;
                }

                // ✅ Assigner le rôle Admin
                var roleResult2 = await _userManager.AddToRoleAsync(admin, adminRole);
                if (!roleResult2.Succeeded)
                {
                    _logger.LogError("❌ Erreur lors de l'assignation du rôle Admin");
                    return;
                }

                _logger.LogInformation("✅ Compte admin créé avec succès : {Email}", adminEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Erreur lors du seeding du compte admin");
            }
        }
    }
}