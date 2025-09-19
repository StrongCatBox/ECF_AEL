Projet Auto-École - ECF CRUD
----------------------------

 Réalisé par : SALAMOVA JEINA


Description :
-------------
Ce projet est une application Web API + Frontend permettant de gérer :
- Les élèves (ajout)
- Les moniteurs (ajout, activation/désactivation, suppression)
- Les véhicules (ajout, état dispo/révision, suppression)
- Les modèles de véhicules (ajout, liste)
- Les leçons de conduite (réservation)

Lorsqu'une leçon est réservée, la date choisie est automatiquement ajoutée dans la table CALENDRIER.


Prérequis :
-----------
- Visual Studio 2022 avec le SDK .NET 8
- SQL Server + SQL Server Management Studio
- Navigateur moderne (Chrome, Edge, Firefox…)

###IMPORTANT D'AJOUTER FICHIER appsettings.json et mettre ça dedans en changeant le serveur, user et mdp.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhostxxx;Database=ECF_AEL;User ID=xx;Password=xxx;TrustServerCertificate=True;Encrypt=False"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}

```


Fonctionnalités testables :
---------------------------
- Ajouter les élèves
- Ajouter, lister, activer/désactiver, supprimer les moniteurs
- Ajouter, lister, modifier l’état, supprimer les véhicules
- Ajouter et lister les modèles de véhicules
- Réserver une leçon :
   -> Ajoute automatiquement la date choisie dans la table CALENDRIER

Certaines fonctionnalités demandées dans le sujet n’ont pas pu être intégrées, principalement à cause d’une incohérence ou d’un manque de données dans la base de données fournie (ECF_AEL.bak).

Par exemple :
- Les statistiques sur les réussites/échecs ne sont pas possibles, car la table des élèves ne contient pas d’historique détaillé des examens passés. On ne sait que si l’élève a son code ou sa conduite, mais pas le nombre de tentatives.
- La gestion des jours fériés et de la fermeture du dimanche n’a pas été implémentée directement dans l’application, car la table CALENDRIER ne contient pas d’indicateurs spécifiques.

### le fichier avec tous les documents complementaires sont ajoutées
il contient :
- diagramme de sequence
- vocabulaire
- use case pour la fonction creer une lecon
- maquettes


Auteur :
--------
Projet réalisé par : SALAMOVA JEINA
Formation : Concepteur Développeur d’Applications (AFPA)
