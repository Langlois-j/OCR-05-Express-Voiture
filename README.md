Projet 5 de la formation Open Class Room
# Initialiser (crée le fichier secrets.json)
dotnet user-secrets init

# Ajouter des secrets
dotnet user-secrets set "Admin:Email" "admin@example.com"
dotnet user-secrets set "Admin:Password" "MonMotDePasse123!"

dotnet user-secrets list

<PropertyGroup>
  <UserSecretsId>ton-guid-unique</UserSecretsId>
</PropertyGroup>
