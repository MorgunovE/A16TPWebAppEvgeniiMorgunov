# Librairie en Ligne - Application Web ASP.NET Core MVC

## Description
Cette application est une plateforme de librairie en ligne développée avec ASP.NET Core MVC (.NET 8) permettant la gestion d'un catalogue de livres, des utilisateurs et des paniers d'achat. L'application offre une interface utilisateur intuitive et responsive pour naviguer dans le catalogue de livres, gérer les comptes utilisateurs et effectuer des achats.

## Fonctionnalités

### Catalogue de Livres
- Affichage du catalogue complet des livres
- Filtrage des livres par titre, auteur, genre et prix
- Visualisation détaillée de chaque livre
- Ajout, modification et suppression de livres (fonctionnalités administratives)
- Gestion des images pour les couvertures de livres

### Gestion des Utilisateurs
- Création et gestion des comptes utilisateurs
- Affichage des informations des utilisateurs
- Modification et suppression de profils

### Paniers d'Achat
- Ajout de livres au panier
- Visualisation du contenu du panier avec calcul automatique du total
- Modification des quantités dans le panier
- Suppression d'articles du panier

### Interface Utilisateur
- Design responsive adapté à tous les appareils
- Interface intuitive avec navigation simplifiée
- Thème graphique cohérent et moderne
- Version française de l'interface

## Technologies Utilisées
- **Backend**: ASP.NET Core MVC (.NET 8)
- **Base de données**: SQL Server avec Entity Framework Core
- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap
- **Gestion d'images**: Stockage local des fichiers

## Structure du Projet

### Modèles
- **User**: Représente un utilisateur avec nom, prénom, téléphone et email
- **Livre**: Représente un livre avec titre, description, auteur, genre, prix et quantité
- **Basket**: Représente le panier d'un utilisateur
- **BasketLivre**: Représente la relation entre un panier et un livre avec quantité

### Contrôleurs
- **HomeController**: Gestion de la page d'accueil et des informations générales
- **LivresController**: Gestion du catalogue de livres (CRUD)
- **UsersController**: Gestion des utilisateurs (CRUD)
- **BasketsController**: Gestion des paniers d'achat et des opérations associées

### Vues
Organisation des vues par contrôleur avec fonctionnalités CRUD pour chaque entité.

## Configuration et Installation

### Prérequis
- .NET 8 SDK
- SQL Server
- Visual Studio 2022

### Configuration de la Base de Données
La chaîne de connexion à la base de données est configurée dans `appsettings.json` :
```bash
"ConnectionStrings": { "DefaultConnection": "Data Source=(localdb)\mssqllocaldb;Initial Catalog=A16TPWebAppEvgeniiMorgunov;Integrated Security=True;Encrypt=False;Trust Server Certificate=True" }
```

### Migrations Entity Framework Core
Pour initialiser/mettre à jour la base de données, utilisez les commandes suivantes dans la Package Manager Console :
```bash
Update-Database
```

### Données de test
L'application inclut un mécanisme de données initiales (SeedData) qui peuple la base de données avec des exemples de livres, d'utilisateurs et de paniers.

## Déploiement
1. Clonez le dépôt 
```bash
git clone https://github.com/MorgunovE/A16TPWebAppEvgeniiMorgunov.git
```
2. Ouvrez la solution dans Visual Studio
3. Restaurez les packages NuGet
4. Exécutez les migrations pour créer la base de données
5. Exécutez l'application

## Structure des Dossiers
- `/Controllers`: Contrôleurs MVC
- `/Models`: Classes de modèles
- `/Views`: Vues Razor organisées par contrôleur
- `/Data`: Classes liées à la base de données et aux migrations
- `/wwwroot`: Fichiers statiques (CSS, JavaScript, images)
  - `/css`: Fichiers CSS, y compris les styles personnalisés
  - `/js`: Scripts JavaScript
  - `/images`: Images du site et des livres
  - `/lib`: Bibliothèques tierces

## Auteur
Evgenii Morgunov

## Licence
Ce projet est sous licence MIT.