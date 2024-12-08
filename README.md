Manage Shoes Foot (C#)
========================

L'application "Manage Shoes Foot (C#)" est une application servant de gestion de chaussures de foot.

Docker
------------

- Pré requis :
    - Téléchargement : https://www.docker.com/ (Download For Windows - AMD64)
    - Installation ...
    - Vérification de la présence de docker et de docker compose (éxecution des commandes suivantes) :

```bash
$ docker --version
```

```bash
$ docker-compose --version
```

- Lancement du docker-compose :
    - Placement dans le dossier du fichier "docker-compose.yml" (./Docker/)
    - Execution (via le terminal de commande) de la commande suivante : 

```bash
$ docker-compose up
```

- Ouverture de PhpMyAdmin : 
    - Url : http://localhost:8080/
    - User : root
    - Password : 123456

- Accès à la base de données : manageshoesfootdb

- Importer 'joueur_category.sql', 'joueur_contact.sql' et 'joueur_product.sql' dans la base de données 'manageshoesfootdb'

GIT
------------

- Récupération du projet (remote vers local) :
    - Placement dans le dossier du fichier (./)
    - Execution (via le terminal de commande) de la commande suivante : 

```bash
$ git clone https://github.com/osscoco/Asp-.NET-Core-Bootstrap.git
```

ENVS
------------

- Modifier la connectionString :
    - Ouverture du fichier "appsettings.json" dans le dossier (./TP_Corentin_Maxence/appsettings.json)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;user id=dbuser;pwd=123456;database=manageshoesfootdb;persistsecurityinfo=True"
  }
}
```

- Modifier également tous les appels de MysqlConnection (dans les fichiers controlleurs) car le projet n'est pas maintenu pour regrouper la modification de la connectionString à un seul endroit.

API WEB
------------

- Lancement de l'API WEB avec IIS Express (dans Visual Studio) ou :
    - Placement dans le dossier (./TP_Corentin_Maxence/)
    - Execution (via le terminal de commande) de la commande suivante : 

```bash
$ dotnet run
```