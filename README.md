# Express Voiture

Express Voiture est une application de gestion de voitures d'occasion. Elle permet aux utilisateurs de gérer l'inventaire de véhicules, leurs réparations et leur mise en vente.

## Fonctionnalités

* Affichage du catalogue des voitures en vente
* Ajout, modification et suppression de voitures
* Gestion des marques et modèles de véhicules
* Suivi des réparations et de leur coût
* Upload de photos de véhicules
* Marquage des voitures comme vendues ou en historique
* Authentification des utilisateurs avec rôle administrateur


## Prérequis

Avant de pouvoir exécuter l'application Express Voiture, assurez-vous d'avoir les éléments suivants installés :

* **.NET SDK** (version 8.0 ou supérieure)
* **Entity Framework Core** (version 8.0)
* **SQL Server** (LocalDB ou Express)

## Structure du projet

* `Program.cs` : Le point d'entrée de l'application
* `Controllers/` : Les contrôleurs de l'application pour la gestion des routes et des actions
* `Models/` : Les modèles de données utilisés dans l'application
  * `Entities/` : Les entités de la base de données (Car, CarBrand, CarModel)
* `Data/` : Les classes de contexte et les migrations pour la gestion de la base de données
* `Views/` : Les vues de l'application pour l'affichage des informations
  * `Cars/` : Vues de gestion des voitures
  * `CarBrands/` : Vues de gestion des marques
  * `Shared/` : Vues partagées et layout
* `wwwroot/` : Les fichiers statiques tels que les feuilles de style CSS et les images
  * `css/` : Feuilles de style personnalisées
  * `img/user/` : Photos des véhicules uploadées

## Configuration

1. Clonez le dépôt Git d'Express Voiture sur votre machine locale.

```bash
git clone https://github.com/Langlois-j/OCR-05-Express-Voiture.git
cd OCR-05-Express-Voiture
```

2. Ouvrez le projet dans votre éditeur de code.

3. Configurez la chaîne de connexion à votre base de données SQL Server dans le fichier `appsettings.json`.

4. Configurez les secrets utilisateur pour l'administrateur :

```bash
dotnet user-secrets init
dotnet user-secrets set "Admin:Email" "admin@expressvoiture.fr"
dotnet user-secrets set "Admin:Password" "VotreMotDePasse123!"
```

5. Ouvrez une console de commande et accédez au répertoire racine du projet.

6. Exécutez la commande suivante pour appliquer les migrations et créer la base de données :

```bash
dotnet ef database update
```

7. Exécutez la commande suivante pour démarrer l'application :

```bash
dotnet run
```

8. Accédez à l'application dans votre navigateur à l'adresse indiquée dans la console (généralement `https://localhost:7000`).

## Connexion administrateur

Pour accéder aux fonctionnalités d'administration, connectez-vous avec l'email et le mot de passe configurés dans les secrets utilisateur.


## Auteur

* Langlois julien

## Contexte

Ce projet a été développé dans le cadre du **Projet 5** de la formation **OpenClassrooms** - Développeur d'application .NET.

## Licence

Ce projet est développé à des fins éducatives dans le cadre de la formation.

